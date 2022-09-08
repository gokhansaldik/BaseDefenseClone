using System;
using Data.ValueObject;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class SaveLoadSignals : MonoSingleton<SaveLoadSignals>
    {
        //TODO:Save Atacağın datalara göre sinyal aç
     
        public UnityAction<ExampleSaveData,int> onSaveExampleData=delegate {  };
        public Func<string,int,ExampleSaveData> onLoadExampleData= delegate { return default;};
    }
}