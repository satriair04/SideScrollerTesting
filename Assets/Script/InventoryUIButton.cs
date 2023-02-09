using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryUIButton : MonoBehaviour
{
    //1. Pass itemData kesini ...
    //2. Method pass itemData dari sini ke InventoryUI untuk ditampilkan ...

    [SerializeField] private Image itemThumbnail;
    [SerializeField] private Transform itemName;

    [SerializeField] private ItemData itemData;
    private Button thisButton;

    private void Awake()
    {
        thisButton = this.gameObject.GetComponent<Button>();
        gameObject.SetActive(false);
    }

    public void SetItemData(ItemData item)
    {
        itemData = item;
        itemThumbnail.sprite = itemData.itemThumbnail;
        itemThumbnail.enabled = true;
        itemName.GetComponent<TextMeshProUGUI>().text = itemData.itemName;
        SetOnClickButtonEvent();
    }

    private void SetOnClickButtonEvent()
    {
        if(itemData == null)
        {
            return;
        }

        //Set onClick event balik ke InventoryUI untuk tombolnya
        thisButton.onClick.AddListener(() => {
            FindObjectOfType<InventoryUI>().UpdateItemDataDetail(itemData);
        });

        
    }
}
