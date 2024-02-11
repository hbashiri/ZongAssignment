using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Player
{
    public class MainPlayer : MonoBehaviour
    {
        public static MainPlayer Instance { get; private set; }
        public TeleportationProvider TeleportationProvider;
        private GameObject leftHandGrabItem;
        private GameObject rightHandGrabItem;

        public GameObject LeftHandGrabItem => leftHandGrabItem;
        public GameObject RightHandGrabItem => rightHandGrabItem;
        
        private void Awake()
        {
            Instance = this;
            TeleportationProvider = GetComponentInChildren<TeleportationProvider>();
        }

        public void AssignHandGrabItem(bool isLeft, GameObject item)
        {
            if (isLeft)
            {
                leftHandGrabItem = item;
            }
            else
            {
                rightHandGrabItem = item;
            }
        }
    }
}