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
            var teleportationRequest = new TeleportRequest()
            {
                destinationPosition = checkPoint.position,
                destinationRotation = checkPoint.rotation
            };

            MainPlayer.Instance.TeleportationProvider.QueueTeleportRequest(teleportationRequest);
        }

        protected override void OnInstrumentExit(InstrumentBase instrument)
        {
        }
    }
}