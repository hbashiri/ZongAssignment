using System;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace VrScripts
{
    public class HandAnimationManager : MonoBehaviour
    {
        [SerializeField] private InputActionProperty pinchAnimationAction;
        [SerializeField] private InputActionProperty gripAnimationAction;
        private Animator _handAnimator;
        private AudioSource _audioSource;
        private bool isLeft;

        private void Awake()
        {
            _handAnimator = GetComponent<Animator>();
            pinchAnimationAction.action.performed += PinchAction;
            gripAnimationAction.action.performed += GripAction;
        }

        private void Start()
        {
            if (isLeft)
            {
                MainPlayer.Instance.LeftHand = this;
            }
            else
            {
                MainPlayer.Instance.RightHand = this;
            }
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

        public void PlaySfx()
        {
            _audioSource.Play();
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