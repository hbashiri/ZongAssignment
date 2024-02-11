using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        Instance = this;
    }

    public void ActivateMainMenu()
    {
        gameObject.SetActive(true);
    }

    public void DeactivateMainMenu()
    {
        gameObject.SetActive(false);
        UiInstrumentPanel.Instance.HideAllInstruments();
    }
    
    ///////////
    /// statics
    ///////////
    
    public static MainMenu Instance { get; private set; }
}
