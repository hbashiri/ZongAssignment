using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class UiInstrumentItem : MonoBehaviour
{
    [SerializeField] private Image icon;

    private InstrumentBase _item;
    private UiInstrumentPanel _uiInstrumentPanel;
    private bool _isOpen;

    public void Setup(UiInstrumentPanel uiInstrumentPanel, InstrumentBase item)
    {
        icon.sprite = item.GetIconImage();
        _item = item;
        _uiInstrumentPanel = uiInstrumentPanel;
        GetComponent<Button>().onClick.AddListener(OnClick);
        _uiInstrumentPanel.SubscribeOnSelectInstrument(HideInstrument);
        item.gameObject.SetActive(false);
    }

    public void OnItemRetrieve()
    {
        _item = null;
        MainMenu.Instance.DeactivateMainMenu();
        Debug.Log("Destroy UiItem");
        Destroy(gameObject);
    }

    private void OnClick()
    {
        if (_isOpen)
        {
            HideInstrument();
            return;
        }

        _isOpen = true;
        _uiInstrumentPanel.HideAllInstruments();
        _item.gameObject.SetActive(true);
        _item.transform.position = _uiInstrumentPanel.InstrumentSpawnPoint.position;
    }

    private void HideInstrument()
    {
        _isOpen = false;
        if (_item == null) return;
        _item.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _uiInstrumentPanel.UnSubscribeOnSelectInstrument(HideInstrument);
    }
}
