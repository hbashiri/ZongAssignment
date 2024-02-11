using System;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class UiInstrumentPanel : MonoBehaviour
{
    [SerializeField] private GameObject instrumentsSubCategory;
    [SerializeField] private UiInstrumentItem uiInstrumentItemPrefab;
    [SerializeField] private Transform instrumentSpawnPoint;
    
    private Action _onSelectInstrument;

    public Transform InstrumentSpawnPoint => instrumentSpawnPoint;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnInstrumentsClicked);
    }

    public void SubscribeOnSelectInstrument(Action action)
    {
        _onSelectInstrument += action;
    }

    public void UnSubscribeOnSelectInstrument(Action action)
    {
        _onSelectInstrument -= action;
    }

    public void HideAllInstruments()
    {
        _onSelectInstrument?.Invoke();
    }

    private void OnInstrumentsClicked()
    {
        if ((MainPlayer.Instance.LeftHandGrabItem && MainPlayer.Instance.LeftHandGrabItem.TryGetComponent<InstrumentBase>(out var instrumentBase))
            || (MainPlayer.Instance.RightHandGrabItem && MainPlayer.Instance.RightHandGrabItem.TryGetComponent<InstrumentBase>(out instrumentBase)))
        {
            instrumentBase.AddItemToInventory();
        }
    }
    
    public UiInstrumentItem AddInstrumentItem(InstrumentBase item)
    {
        var instantiatedItem = Instantiate(uiInstrumentItemPrefab, instrumentsSubCategory.transform);
        Canvas.ForceUpdateCanvases();
        instantiatedItem.Setup(this, item);
        return instantiatedItem;
    }
    
    
    
    ///////////
    /// statics
    ///////////

    public static UiInstrumentPanel Instance {get; private set;}
}
