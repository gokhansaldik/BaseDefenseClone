using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class EnemySignals : MonoSingleton<EnemySignals>
    {
        public UnityAction onPlayerDamage = delegate { };  
        public UnityAction<Transform> onEnemyDie = delegate { };
        //public UnityAction onEnemyToMoney= delegate { };
      
    }
}