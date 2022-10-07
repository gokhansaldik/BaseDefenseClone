using System.Collections.Generic;
using UnityEngine;

namespace Inheritance
{
    public class AiManager : MonoBehaviour
    {
        public List<AiBase> MoneyWorker = new List<AiBase>();
        public List<AiBase> AmmoWorker = new List<AiBase>();
    }
}