using System;
using Data.ValueObject;
using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class EnemySignals : MonoSingleton<EnemySignals>
    {
        public UnityAction onPlayerDamage = delegate { };  // Enemy Player damage attiginda tetiklenen signal.
        public UnityAction<Transform> onEnemyDie = delegate { };
      
    }
}