using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiWeaponPanel : MonoBehaviour
{
    [SerializeField] private GameObject weaponsSubCategory;
    
    private bool _isOpened;
    
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnWeaponsClicked);
    }
    
    private void OnWeaponsClicked()
    {
        _isOpened = !_isOpened;
        weaponsSubCategory.SetActive(_isOpened);
    }

    public void CollapseWeaponCategory()
    {
        _isOpened = false;
        weaponsSubCategory.SetActive(_isOpened);
    }
}
