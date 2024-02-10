using System;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Player
{
    public class InstrumentBase : MonoBehaviour
    {
        [SerializeField] private string message;
        [SerializeField] private GameObject panelPrefab;
        [SerializeField] private Vector3 panelPositionOffset;

        private XRGrabInteractable _grableInteractable;
        private GameObject _messagePanel;

        private void Awake()
        {
            _grableInteractable = GetComponent<XRGrabInteractable>();
            _grableInteractable.firstSelectEntered.AddListener( _ => _messagePanel.SetActive(false));
            _grableInteractable.lastSelectExited.AddListener(SetMessagePanel);
            _grableInteractable.activated.AddListener(_ => _grableInteractable.movementType = XRBaseInteractable.MovementType.Instantaneous);
            _grableInteractable.deactivated.AddListener(_ => _grableInteractable.movementType = XRBaseInteractable.MovementType.VelocityTracking);
        }

        private void SetMessagePanel(SelectExitEventArgs eventData)
        {
            var playerPosition = MainPlayer.Instance.transform.position;
            _messagePanel.transform.rotation = Quaternion.LookRotation(new Vector3(playerPosition.x,
                transform.position.y, playerPosition.z) - transform.position, Vector3.up);
            _messagePanel.SetActive(true);
        }

        private void Start()
        {
            _messagePanel = Instantiate(panelPrefab, transform.position + panelPositionOffset, Quaternion.identity);
            _messagePanel.GetComponentInChildren<TextMeshPro>().text = message;
        }

        private void Update()
        {
            _messagePanel.transform.position = transform.position + panelPositionOffset;
        }
    }
}