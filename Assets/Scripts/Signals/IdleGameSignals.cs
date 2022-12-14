using System;
using Data.ValueObject;
using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class IdleGameSignals : MonoSingleton<IdleGameSignals>
    {
        #region Mine Signals

        public UnityAction onBaseAreaBuyedItem = delegate { };
        public Func<GameObject> onGetMineTarget = delegate { return default; };
        public Func<GameObject> onGetMineStackTarget = delegate { return default; };
        public UnityAction<GameObject> onAddDiamondStack = delegate { };

        #endregion

        #region Base Signals

        public UnityAction onGettedBaseData = delegate { };
        public Func<GameObject> onGetAmmo = delegate { return default; };
        public UnityAction onOpenPlayerHealthBar = delegate { };
        public UnityAction onClosePlayerHealthBar = delegate { };

        #endregion

        #region Turret Signals

        public Func<TurretNameEnum, TurretData> onTurretData = delegate { return default; };
        public Func<TurretNameEnum, int> onPayedTurretData = delegate { return default; };
        public UnityAction<TurretNameEnum, int> onTurretAreaBuyedItem = delegate { };
        public UnityAction<GameObject> onPlayerInTurret = delegate { };
        public UnityAction<GameObject> onPlayerOutTurret = delegate { };
        public UnityAction<GameObject, GameObject> onSendAmmoInStack=delegate{};
        public UnityAction<GameObject> onAddBulletBoxStack = delegate { };
        #endregion
    }
}