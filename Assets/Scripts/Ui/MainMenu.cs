using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;

    private bool _isOpened;
    private void Awake()
    {
        Instance = this;
    }

    public void ActivateMainMenu(Transform menuPosition)
    {
        transform.position = menuPosition.position;
        transform.rotation = menuPosition.rotation;
        mainMenuPanel.SetActive(true);
        _isOpened = true;
    }

    public void DeactivateMainMenu()
    {
        mainMenuPanel.SetActive(false);
        UiInstrumentPanel.Instance.HideAllInstruments();
        _isOpened = false;
    }

    public void ToggleMainMenu(Transform menuPosition)
    {
        if (_isOpened)
        {
            DeactivateMainMenu();
        }
        else
        {
            ActivateMainMenu(menuPosition);
        }
    }
    
    ///////////
    /// statics
    ///////////
    
    public static MainMenu Instance { get; private set; }
}
