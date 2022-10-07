using UnityEngine;

namespace Commands.Level
{
    public class ClearActiveLevelCommand
    {
        #region Self Variables

        #region Private Variables

        private GameObject _levelHolder;

        #endregion
        #endregion
        public ClearActiveLevelCommand(ref GameObject levelHolder)
        {
            _levelHolder = levelHolder;
        }
        public void Execute()
        {
            Object.Destroy(_levelHolder.transform.GetChild(0).gameObject);
        }
    }
}