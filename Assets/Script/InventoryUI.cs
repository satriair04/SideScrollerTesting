using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryUI : MonoBehaviour
{
    [Header("Property")]
    public Transform itemNameTMP;
    public Transform itemDescriptionTMP;
    public Image itemImageSprite;

    [Header("Parent n Child for Tombol Item")]
    [SerializeField] private Transform itemButtonParentSpawner;
    [SerializeField] private Transform itemButtonChildTemplate;
    private bool isActiveState = false;         //Default
  
    private Transform itemButtonChild;

    private void Awake()
    {
        //NB : Jangan ubah nama child dari Transform itemButtonSpawnBox
        itemButtonChild = itemButtonChildTemplate;
        //Line kode rusak :
        //itemButtonChild = FindObjectOfType<InventoryUIButton>().gameObject.transform;
    }

    void Start()
    {
        this.gameObject.SetActive(isActiveState);
    }

    public void ToogleUI()
    {
        isActiveState = !isActiveState;
        gameObject.SetActive(isActiveState);
        if(isActiveState == true)
        {
            UpdateItemDataList();
        }
        
    }

    public void UpdateItemDataDetail(ItemData item)
    {
        //Tampilkan detail suatu ItemData yang dipilih
        itemNameTMP.GetComponent<TextMeshProUGUI>().text = item.itemName;
        itemImageSprite.sprite = item.itemThumbnail;
        itemDescriptionTMP.GetComponent<TextMeshProUGUI>().text = item.itemDesc;
    }

    private void UpdateItemDataList()
    {
        ResetItemDataList();
        //Update isi List<ItemData> ketika diakses/ambil item ke InventoryUI
        foreach (ItemData item in InventoryManager.Instance.listItem)
        {
            Transform spawnedButton = Instantiate(itemButtonChild, itemButtonParentSpawner);
            spawnedButton.GetComponent<InventoryUIButton>().SetItemData(item);
            spawnedButton.gameObject.SetActive(true);
        }
    }

    private void ResetItemDataList()
    {
        //Hapus semua Tombol lama
        foreach (Transform child in itemButtonParentSpawner)
        {
            if (child == itemButtonChild)
            {
                continue;
            }
            Destroy(child.gameObject);
        }
    }

    private void OnEnable()
    {
        InventoryManager.Instance.callbackRefreshInventory += UpdateItemDataList;
        //.callbackInventoryUpdateUI += RefreshInventoryUI;
    }

    private void OnDisable()
    {
        InventoryManager.Instance.callbackRefreshInventory -= UpdateItemDataList;
        //playerInventory.callbackInventoryUpdateUI -= RefreshInventoryUI;
    }

    /*
    private void ClearItemDataList()
    {
        //Hapus dulu tombol lama yang ada agar bisa update InventoryUI ke terbaru
        while (buttonSpawnLocation.transform.GetChild(0) != null)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
    */
}
