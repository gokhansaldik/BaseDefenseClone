using Data.UnityObject;
using UnityEngine;

namespace Commands.Pool
{
    public class PoolAddCommand
    {
        #region Self Variables

        #region Private Variables

        private CD_Pool _cdPool;
        private GameObject _gameObject;
        private Transform _managerTransform;

        #endregion

        #endregion

        public PoolAddCommand(ref CD_Pool cdPool, ref Transform managertransform, ref GameObject gameObject)
        {
            _cdPool = cdPool;
            _gameObject = gameObject;
            _managerTransform = managertransform;
        }

        public void Execute()
        {
            var pooldata = _cdPool.PoolDataList;
            for (var i = 0; i < pooldata.Count; i++)
            {
                _gameObject = new GameObject();
                _gameObject.transform.parent = _managerTransform;
                _gameObject.name = pooldata[i].PoolType.ToString();
                for (var j = 0; j < pooldata[i].ObjectCount; j++)
                {
                    var obj = Object.Instantiate(pooldata[i].Pref, _managerTransform.GetChild(i));
                    obj.SetActive(false);
                }
            }
        }
    }
}