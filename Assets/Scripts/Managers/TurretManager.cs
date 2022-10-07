using System.Collections.Generic;
using Keys;
using Signals;
using UnityEngine;


namespace Managers
{
    public class TurretManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public bool HasOwner = false;
        public bool IsPlayerUsing = false;

        public List<Transform> AmmoBoxList = new List<Transform>();

        #endregion

        #region Serialized Variables

       // [SerializeField] private TurretRangeController rangeController;

        [SerializeField] private Transform turretPlayerParentObj;
        [SerializeField] private Transform turretRotatableObj;

        #endregion

        #region Private Variables

        private float _xValue, _zValue;

        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
        }

        // public Material GetMaterial() => Resources.Load<Material>("Materials/TurretFloor/" +
        //                                                           (LevelSignals.Instance.onGetCurrentModdedLevel() + 1)
        //                                                           .ToString());

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            //PlayerSignals.Instance.onEnemyDie += rangeController.OnRemoveFromTargetList;
            InputSignals.Instance.onInputDragged += OnInputDragged;
        }

        private void UnsubscribeEvents()
        {
            //PlayerSignals.Instance.onEnemyDie -= rangeController.OnRemoveFromTargetList;
            InputSignals.Instance.onInputDragged -= OnInputDragged;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        public void PlayerUseTurret(Transform player)
        {
            IsPlayerUsing = true;
            player.parent = turretPlayerParentObj;
            //player.transform.DOMove(turretOwner.position, 1f);
            player.transform.position = turretPlayerParentObj.position;
            player.transform.rotation = turretPlayerParentObj.rotation;
        }

        public void PlayerLeaveTurret(Transform player)
        {
            IsPlayerUsing = false;

            player.parent = null;
        }

        public void OnInputDragged(InputParams param)
        {
            _xValue = param.XValue;
            _zValue = param.ZValue;
        }

        private void FixedUpdate()
        {
            if (!IsPlayerUsing)
            {
                return;
            }

            if (_xValue.Equals(0))
            {
                return;
            }

            if (_zValue < -0.9f)
            {
                PlayerSignals.Instance.onPlayerUseTurret?.Invoke(false);
            }

            turretRotatableObj.rotation = Quaternion.Euler(new Vector3(0, 30 * _xValue * -1, 0)); //slerp
        }
    }
}