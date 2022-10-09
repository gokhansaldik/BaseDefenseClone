using System.Collections.Generic;
using Controllers;
using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class ParticleSignals : MonoSingleton<ParticleSignals>
    {
        public UnityAction<ParticleType, Vector3, Quaternion> onPlayParticle = delegate {  };
        public UnityAction<ParticleType, Vector3, Quaternion, Color> onPlayParticleWithSetColor = delegate {  };
        public UnityAction<ParticleType> onStopParticle = delegate {  };
        public UnityAction<List<ParticleEmitController>> onStopAllParticle = delegate {  };
    }
}
