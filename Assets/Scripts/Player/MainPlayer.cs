using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Player
{
    public class MainPlayer : MonoBehaviour
    {
        public static MainPlayer Instance { get; private set; }
        public LocomotionSystem LocomotionSystem;
        public TeleportationProvider TeleportationProvider;

        private void Awake()
        {
            Instance = this;
            LocomotionSystem = GetComponentInChildren<LocomotionSystem>();
            TeleportationProvider = GetComponentInChildren<TeleportationProvider>();
        }
    }
}