using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using VrScripts;

namespace Player
{
    public class MainPlayer : MonoBehaviour
    {
        public static MainPlayer Instance { get; private set; }
        public HandAnimationManager LeftHand;
        public HandAnimationManager RightHand;

        [SerializeField] private Transform menuPlaceHolder;
        [SerializeField] private InputActionProperty menuInputAction;
        
        private TeleportationProvider _teleportationProvider;
        private XRGrabInteractable leftHandGrabItem;
        private XRGrabInteractable rightHandGrabItem;

        public XRGrabInteractable LeftHandGrabItem => leftHandGrabItem;
        public XRGrabInteractable RightHandGrabItem => rightHandGrabItem;
        public Transform MenuPlaceHolder => menuPlaceHolder;

        private void Awake()
        {
            Instance = this;
            _teleportationProvider = GetComponentInChildren<TeleportationProvider>();
            menuInputAction.action.performed += OnMenuClicked;
        }

        public void AssignHandGrabItem(bool isLeft, XRGrabInteractable item)
        {
            if (isLeft)
            {
                leftHandGrabItem = item;
                LeftHand.PlaySfx();
            }
            else
            {
                rightHandGrabItem = item;
                RightHand.PlaySfx();
            }
        }

        public void ReleaseGrabedItem(bool isLeft)
        {
            if (isLeft)
            {
                leftHandGrabItem = null;
            }
            else
            {
                rightHandGrabItem = null;
            }
        }

        public void ResetToCheckPoint(Transform checkPoint)
        {
            TeleportPlayer(checkPoint);
            MainMenu.Instance.ActivateMainMenu(checkPoint.position,
                checkPoint.position + checkPoint.forward * 3 + checkPoint.up,
                checkPoint.rotation);
        }
        
        public void TeleportPlayer(Transform positionTransform)
        {
            var teleportationRequest = new TeleportRequest()
            {
                destinationPosition = positionTransform.position,
                destinationRotation = positionTransform.rotation
            };
            _teleportationProvider.QueueTeleportRequest(teleportationRequest);
        }
        

        private void OnMenuClicked(InputAction.CallbackContext inputData)
        {
            MainMenu.Instance.ToggleMainMenu();
        }
    }
}