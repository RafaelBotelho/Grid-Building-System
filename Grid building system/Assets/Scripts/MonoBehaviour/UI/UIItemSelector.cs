using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemSelector : MonoBehaviour
{
    #region Variables

    [SerializeField] private SO_Item _item;
    [SerializeField] private TextMeshProUGUI _itemText;
    [SerializeField] private Image _itemIcon;

    #endregion

    #region Monobehaviour

    private void Start()
    {
        Initialize(_item);
    }

    #endregion
    
    #region Methods
    
    private void Initialize(SO_Item newItem)
    {
        _item = newItem;
        
        if (_item.SpriteIcon)
            _itemIcon.sprite = _item.SpriteIcon;
        else
            _itemText.text = _item.Name;
    }

    public void SelectItem()
    {
        EventManager.OnSelectItem?.Invoke(_item);
    }

    #endregion
}