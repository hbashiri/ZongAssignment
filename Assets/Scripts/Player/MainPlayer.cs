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
        [HideInInspector] public HandAnimationManager LeftHand;
        [HideInInspector] public HandAnimationManager RightHand;
        
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private InputActionProperty menuInputAction;
        [SerializeField] private float uiDistance;
        private TeleportationProvider _teleportationProvider;
        private XRGrabInteractable leftHandGrabItem;
        private XRGrabInteractable rightHandGrabItem;

        public XRGrabInteractable LeftHandGrabItem => leftHandGrabItem;
        public XRGrabInteractable RightHandGrabItem => rightHandGrabItem;
        public Transform CameraTransform => cameraTransform;

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
                checkPoint.position + new Vector3(0f, cameraTransform.transform.position.y, uiDistance),
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

        public Vector3 GetDefaultMenuPosition()
        {
            var cameraPosition = cameraTransform.position + uiDistance * 
                new Vector3(Mathf.Sin(cameraTransform.eulerAngles.y* Mathf.Deg2Rad), 0f,
                    Mathf.Cos(cameraTransform.eulerAngles.y * Mathf.Deg2Rad));
            return cameraPosition;
        }

        private void OnMenuClicked(InputAction.CallbackContext inputData)
        {
            MainMenu.Instance.ToggleMainMenu();
        }
    }
}