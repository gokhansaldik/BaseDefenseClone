﻿using System;
using Abstract;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


namespace AIBrains
{ 
    public class EnemyAIBrain : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public Transform PlayerTarget;

        public Transform _turretTarget;
        
        public Transform MineTarget;
        
        public NavMeshAgent NavMeshAgent;
        
        public int Health;
        
        public EnemyType EnemyType;
        #endregion

        #region Serialized Variables,

        [SerializeField] private EnemyType enemyType;
        [SerializeField] private EnemyPhysicsController physicsController;
        
        #endregion

        #region Private Variables

        private int _levelID;
        private EnemyTypeData _data;
        private EnemyAiData _AIData;
        private int _damage;
        private float _attackRange;
        private float _attackSpeed;
        private float _moveSpeed;
        private Color _enemyColor;
        private float _chaseSpeed;
        private Vector3 _scaleSize;
        private float _navMeshRadius;
        private float _navMeshHeight;
        private Animator _animator;
        private StateBehaviour.StateMachine _stateMachine;
        private Transform _spawnPoint;
        private Chase chase;
        #endregion
        
        #endregion
        private void Awake()
        {
            _levelID = 0;
            // _levelID = levelSignals.Instance.OnGetLevel();
            _AIData = GetEnemyAIData();
            _data = GetEnemyData();
            SetEnemyData();
            GetStatesReferences();
        }
        private EnemyAiData GetEnemyAIData() => Resources.Load<CD_EnemyAI>("Data/CD_Enemy").EnemyAIData;
        private EnemyTypeData GetEnemyData() => _AIData.EnemyList[(int)enemyType];
        private void SetEnemyData()
        {  
            Health = _data.Health;
            _damage = _data.Damage;
            _attackRange = _data.AttackRange;
            _attackSpeed = _data.AttackSpeed;
            _chaseSpeed = _data.ChaseSpeed;
            _moveSpeed = _data.MoveSpeed;
            _enemyColor = _data.BodyColor;
            _scaleSize = _data.ScaleSize;
            _navMeshRadius = _data.NavMeshRadius;
            _navMeshHeight = _data.NavMeshHeight;
            _spawnPoint = _AIData.SpawnPositionList[_levelID];
            _turretTarget = _AIData.SpawnPositionList[_levelID].GetChild(Random.Range(0, _AIData.SpawnPositionList[_levelID].childCount));
        }
        private void GetStatesReferences()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>(); 
            _animator = GetComponent<Animator>();

            var search = new Search(this, NavMeshAgent, _spawnPoint);
            var move = new Move(NavMeshAgent,_animator,this,_moveSpeed); 
            var attack = new Attack(NavMeshAgent,_animator,this,_attackRange);
            var death = new Death(NavMeshAgent,_animator,this);
            chase = new Chase(NavMeshAgent,_animator,this,_attackRange,_chaseSpeed);
            var moveToBomb = new MoveToBomb(NavMeshAgent,_animator,this,_attackRange,_chaseSpeed);
            _stateMachine = new StateBehaviour.StateMachine();
          
            At(search,move,HasTurretTarget());
            At(move,chase,HasTarget());  
            At(chase,attack,AttackThePlayer()); 
            At(attack,chase,()=>attack.InPlayerAttackRange()==false);
            At(chase,move,TargetNull());
            
            _stateMachine.AddAnyTransition(death,()=> physicsController.AmIdead());
            _stateMachine.AddAnyTransition(moveToBomb,()=> physicsController.IsBombInRange());
           
            _stateMachine.SetState(search);
            void At(IState to,IState from,Func<bool> condition) =>_stateMachine.AddTransition(to,from,condition);

            Func<bool> HasTurretTarget() => () => _turretTarget != null;
            Func<bool> HasTarget() => () => PlayerTarget != null;
            Func<bool> AttackThePlayer() => () => PlayerTarget != null && chase.InPlayerAttackRange();
            Func<bool> TargetNull() => () => PlayerTarget is null;
        }

        private void Update() =>  _stateMachine.UpdateIState();
    }
}