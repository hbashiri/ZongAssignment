using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace Player
{
    public class MainPlayer : MonoBehaviour
    {
        public static MainPlayer Instance { get; private set; }

        [SerializeField] private Transform menuPlaceHolder;
        [SerializeField] private InputActionProperty menuInputAction;
        
        private TeleportationProvider _teleportationProvider;
        private GameObject leftHandGrabItem;
        private GameObject rightHandGrabItem;
        
        public GameObject LeftHandGrabItem => leftHandGrabItem;
        public GameObject RightHandGrabItem => rightHandGrabItem;
        public TeleportationProvider TeleportationProvider => _teleportationProvider;
        public Transform MenuPlaceHolder => menuPlaceHolder;
        
        private void Awake()
        {
            Instance = this;
            _teleportationProvider = GetComponentInChildren<TeleportationProvider>();
            menuInputAction.action.performed += OnMenuClicked;
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

        private void OnMenuClicked(InputAction.CallbackContext inputData)
        {
            MainMenu.Instance.ToggleMainMenu(MenuPlaceHolder);
        }
    }
}