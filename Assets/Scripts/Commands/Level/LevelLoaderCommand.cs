using UnityEngine;
using UnityEngine.Android;

namespace Commands.Level
{
    public class LevelLoaderCommand
    {
        #region Self Variables

        #region Private Variables

        private GameObject _levelHolder;

        #endregion

        #endregion

        public LevelLoaderCommand(ref GameObject levelHolder)
        {
            _levelHolder = levelHolder;
        }

        public void Execute(int _levelId)
        {
            // TODO: sahne resources cekme.
            //Object.Instantiate(Resources.Load<GameObject>($"Prefabs/LevelPrefabs/Level {_levelId}"),_levelHolder.transform);
        }
    }
}