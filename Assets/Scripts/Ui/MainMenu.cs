using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Ui;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UiInstrumentPanel uiInstrumentPanel;
    [SerializeField] private UiWeaponPanel uiWeaponPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private float distanceToDisappearMenu;

    private Transform _playerPosition;
    private bool _isOpened;
    private FadePanel _fadePanelSystem;
    private Vector3 _playerPositionOnSpawn;
    private AudioSource _audioSource;

    public static MainMenu Instance { get; private set; }
    public UiInstrumentPanel InstrumentPanel => uiInstrumentPanel;

    private void Awake()
    {
        Instance = this;
        _fadePanelSystem = GetComponent<FadePanel>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _playerPosition = MainPlayer.Instance.CameraTransform;
    }

    private void Update()
    {
        if (_isOpened && Vector3.Distance(_playerPosition.position, _playerPositionOnSpawn) > distanceToDisappearMenu)
        {
            DeactivateMainMenu();
        }
    }

    public void ActivateMainMenu()
    {
        _playerPositionOnSpawn = _playerPosition.position;
        transform.position = MainPlayer.Instance.GetDefaultMenuPosition();
        transform.rotation = Quaternion.Euler(0f,MainPlayer.Instance.CameraTransform.rotation.eulerAngles.y, 0);
        mainMenuPanel.SetActive(true);
        _fadePanelSystem.FadeIn(()=>{});
        _isOpened = true;
    }
    
    public void ActivateMainMenu(Vector3 customPosition, Vector3 UIPosition, Quaternion customRotation)
    {
        _playerPositionOnSpawn = customPosition;
        transform.position = UIPosition;
        transform.rotation = customRotation;
        mainMenuPanel.SetActive(true);
        _fadePanelSystem.FadeIn(()=>{});
        _isOpened = true;
    }

    public void DeactivateMainMenu()
    {
        _fadePanelSystem.FadeOut(()=>mainMenuPanel.SetActive(false));
        InstrumentPanel.HideAllInstruments();
        _isOpened = false;
    }

    public void ToggleMainMenu()
    {
        if (_isOpened)
        {
            DeactivateMainMenu();
        }
        else
        {
            ActivateMainMenu();
        }
    }

    public void CollapseWeaponCategory()
    {
        uiWeaponPanel.CollapseWeaponCategory();
    }

    public void PlaySfx()
    {
        _audioSource.Play();
    }
}
