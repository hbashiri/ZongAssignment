using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace VrScripts
{
    public class HandAnimationManager : MonoBehaviour
    {
        [SerializeField] private InputActionProperty pinchAnimationAction;
        [SerializeField] private InputActionProperty gripAnimationAction;
        private Animator _handAnimator;

        private void Awake()
        {
            _handAnimator = GetComponent<Animator>();
            pinchAnimationAction.action.performed += PinchAction;
            gripAnimationAction.action.performed += GripAction;
        }

        private void GripAction(InputAction.CallbackContext inputData)
        {
            float gripValue = gripAnimationAction.action.ReadValue<float>();
            _handAnimator.SetFloat("Grip", gripValue);
        }

        private void PinchAction(InputAction.CallbackContext inputData)
        {
            float triggerValue = pinchAnimationAction.action.ReadValue<float>();
            _handAnimator.SetFloat("Trigger", triggerValue);
        }

        private void Update()
        {
            // float triggerValue = pinchAnimationAction.action.ReadValue<float>();
            // _handAnimator.SetFloat("Trigger", triggerValue);
            //
            // float gripValue = gripAnimationAction.action.ReadValue<float>();
            // _handAnimator.SetFloat("Trigger", gripValue);
        }
    }
}