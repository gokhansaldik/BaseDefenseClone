using UnityEngine;

namespace TryScripts
{
    public class EnemySpawn : MonoBehaviour
    {
        public GameObject enemies;
        public float TiempoCreacion = 2f;
        public float RangoCreacion = 2f;
        
        void Start()
        {
            InvokeRepeating ("Crear",0.0f,TiempoCreacion);
        }

       
        public void Crear(){
            Vector2 SpawnPos = new Vector2 (0,0);
            SpawnPos = this.transform.position + Random.onUnitSphere * RangoCreacion;
            SpawnPos = new Vector2 (SpawnPos.x,SpawnPos.y);
        
            GameObject enemy = Instantiate (enemies, SpawnPos, Quaternion.identity);
        }
    }
}
