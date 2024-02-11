using System;
using System.Collections;
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
        [SerializeField] private Sprite iconImage;

        private UiInstrumentItem _uiInstrumentItem;

        private XRGrabInteractable _grableInteractable;
        private GameObject _messagePanel;
        private Rigidbody _rigidbody;
        private bool _boxState;
        private bool _selectState;
        private Coroutine enableMessageCoroutine;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _grableInteractable = GetComponent<XRGrabInteractable>();
            _grableInteractable.firstSelectEntered.AddListener(BallSelected);
            _grableInteractable.lastSelectExited.AddListener(BallUnselected);
            _grableInteractable.selectEntered.AddListener(SelectHandAssign);
            _grableInteractable.selectExited.AddListener(SelectHandRelease);
            _grableInteractable.activated.AddListener(_ => _grableInteractable.movementType = XRBaseInteractable.MovementType.Instantaneous);
            _grableInteractable.deactivated.AddListener(_ => _grableInteractable.movementType = XRBaseInteractable.MovementType.VelocityTracking);
        }

        private void Start()
        {
            _messagePanel = Instantiate(panelPrefab, transform.position + panelPositionOffset, Quaternion.identity);
            _messagePanel.GetComponentInChildren<TextMeshPro>().text = message;
            _messagePanel.transform.GetChild(0).transform.localPosition = panelPositionOffset;
        }

        private void Update()
        {
            _messagePanel.transform.position = transform.position;
        }

        public Sprite GetIconImage()
        {
            return iconImage;
        }
        
        public void EnterTheBox()
        {
            MessagePanelToggle(false);
            _boxState = true;
        }

        public void ExitTheBox()
        {
            _boxState = false;
        }
        
        public void AddItemToInventory()
        {
            _uiInstrumentItem = MainMenu.Instance.InstrumentPanel.AddInstrumentItem(this);
            _rigidbody.isKinematic = true;
            MessagePanelToggle(false);
        }

        public void OnRetrieveItemFromInventory()
        {
            if (_uiInstrumentItem != null)
            {
                _uiInstrumentItem.OnItemRetrieve();
                _uiInstrumentItem = null;
                
            }
            _rigidbody.isKinematic = false;
        }
        
                private void BallSelected(SelectEnterEventArgs eventData)
        {
            _selectState = true;
            MessagePanelToggle(false);
            
            if (_uiInstrumentItem != null)
            {
                OnRetrieveItemFromInventory();
            }
            else
            {
                MainMenu.Instance.ActivateMainMenu();
            }
        }
        
        private void SelectHandRelease(SelectExitEventArgs eventData)
        {
            if (eventData.interactorObject.transform.CompareTag("LeftHand"))
            {
                MainPlayer.Instance.ReleaseGrabedItem(true);
            }
            else if (eventData.interactorObject.transform.CompareTag("RightHand"))
            {
                MainPlayer.Instance.ReleaseGrabedItem(false);
            }
        }

        private void SelectHandAssign(SelectEnterEventArgs eventData)
        {
            if (eventData.interactorObject.transform.CompareTag("LeftHand"))
            {
                MainPlayer.Instance.AssignHandGrabItem(true, _grableInteractable);
            }
            else if (eventData.interactorObject.transform.CompareTag("RightHand"))
            {
                MainPlayer.Instance.AssignHandGrabItem(false, this._grableInteractable);
            }
        }

        private void BallUnselected(SelectExitEventArgs eventData)
        {
            _selectState = false;
            _rigidbody.isKinematic = false;
            if (gameObject.activeSelf)
            {
                enableMessageCoroutine = StartCoroutine(EnableMessage());
            }
        }

        private void MessagePanelToggle(bool state)
        {
            if (!state && enableMessageCoroutine != null)
            {
                StopCoroutine(enableMessageCoroutine);
            }
            _messagePanel.SetActive(state);
        }

        private IEnumerator EnableMessage()
        {
            yield return new WaitForSeconds(1f);
            var playerPosition = MainPlayer.Instance.transform.position;
            _messagePanel.transform.rotation = Quaternion.LookRotation(transform.position - new Vector3(playerPosition.x,
                transform.position.y, playerPosition.z), Vector3.up);
            MessagePanelToggle(true);
        }
    }
}