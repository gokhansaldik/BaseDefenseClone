using UnityEngine;

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
            //Object.Instantiate(Resources.Load<GameObject>($"Prefabs/LevelPrefabs/Level {_levelId}"),_levelHolder.transform);
        }
    }
}