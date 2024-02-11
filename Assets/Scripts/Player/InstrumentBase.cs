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
        [SerializeField] private Sprite iconImage;
        
        public Action ItemSelectionEvent ;
        private UiInstrumentItem _uiInstrumentItem;

        private XRGrabInteractable _grableInteractable;
        private GameObject _messagePanel;
        private bool _boxState;
        private bool _selectState;

        private void Awake()
        {
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
        
        public Sprite GetIconImage()
        {
            return iconImage;
        }

        private void Update()
        {
            _messagePanel.transform.position = transform.position;
        }

        private void BallSelected(SelectEnterEventArgs eventData)
        {
            _selectState = true;
            _messagePanel.SetActive(false);
            MainMenu.Instance.ActivateMainMenu();
        }
        
        private void SelectHandRelease(SelectExitEventArgs eventData)
        {
            if (eventData.interactorObject.transform.CompareTag("LeftHand"))
            {
                print("Release by left hand");
                MainPlayer.Instance.AssignHandGrabItem(true,  null);
            }
            else if (eventData.interactorObject.transform.CompareTag("RightHand"))
            {
                print("Release by right hand");
                MainPlayer.Instance.AssignHandGrabItem(false, null);
            }
        }

        private void SelectHandAssign(SelectEnterEventArgs eventData)
        {
            if (eventData.interactorObject.transform.CompareTag("LeftHand"))
            {
                print("Grab by left hand");
                MainPlayer.Instance.AssignHandGrabItem(true, this.gameObject);
            }
            else if (eventData.interactorObject.transform.CompareTag("RightHand"))
            {
                print("Grab by right hand");
                MainPlayer.Instance.AssignHandGrabItem(false, this.gameObject);
            }
        }

        private void BallUnselected(SelectExitEventArgs eventData)
        {
            _selectState = false;
            Invoke(nameof(EnableMessagePanel), 1f);
            MainMenu.Instance.DeactivateMainMenu();
        }

        private void EnableMessagePanel()
        {
            if (!_boxState && !_selectState)
            {
                var playerPosition = MainPlayer.Instance.transform.position;
                _messagePanel.transform.rotation = Quaternion.LookRotation(transform.position - new Vector3(playerPosition.x,
                    transform.position.y, playerPosition.z), Vector3.up);
                _messagePanel.SetActive(true);
            }
        }

        public void EnterTheBox()
        {
            _messagePanel.SetActive(false);
            _boxState = true;
        }

        public void ExitTheBox()
        {
            _boxState = false;
        }
        
        public void AddItemToInventory()
        {
            _uiInstrumentItem = UiInstrumentPanel.Instance.AddInstrumentItem(this);
            // make it kinematic
        }

        public void OnRetrieveItemFromInventory()
        {
            if (_uiInstrumentItem != null)
            {
                _uiInstrumentItem.OnItemRetrieve();
            }
            //make it Cinematic ih ih ih
        }
    }
}