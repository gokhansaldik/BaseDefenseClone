using Interface;

namespace StateMachine.Enemy
{
    public class AttackState : IState
    {
        #region Self Variables

        #region Private Variables

        private IEnemyController _enemyController;

        #endregion
        #endregion

        public AttackState(IEnemyController enemyController)
        {
            _enemyController = enemyController;
        }
        #region States
        public void OnEnter()
        {
        }
        public void OnExit()
        {
            _enemyController.Animation.AttackAnim(false);
        }
        public void Tick()
        {
            _enemyController.Animation.AttackAnim(true);
        }
        #endregion
    }
}