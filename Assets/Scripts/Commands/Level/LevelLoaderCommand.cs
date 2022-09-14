using UnityEngine;

namespace Command
{
    public class LevelLoaderCommand
    {
        public void InitializeLevel(int _levelID, Transform levelHolder)
        {
            Object.Instantiate(Resources.Load<GameObject>($"Prefabs/LevelPrefabs/level {_levelID}"), levelHolder);
        }
    }
}