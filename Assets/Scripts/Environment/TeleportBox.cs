using Player;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Environment
{
    public class TeleportBox : Box
    {
        [SerializeField] private Transform checkPoint;
        private void Awake()
        {
            
        }
        
        protected override void OnInstrumentEnter(InstrumentBase instrument)
        {
            instrument.AddItemToInventory();
            instrument.EnterTheBox();
            MainPlayer.Instance.ResetToCheckPoint(checkPoint);
        }

        protected override void OnInstrumentExit(InstrumentBase instrument)
        {
            instrument.ExitTheBox();
        }
    }
}