using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public int inventoryLimit = 20;
    public List<ItemData> listItem = new List<ItemData>();

    public UnityAction callbackRefreshInventory;

    private void Awake()
    {
        CreateInstance();
    }

    // Public Method/Function :

    public void AddToInventory(ItemData itemToAdd)
    {
        if(itemToAdd == null)
        {
            return;
        }
        if (listItem.Contains(itemToAdd))
        {
            return;
        }
        if(listItem.Count >= inventoryLimit)
        {
            return;
        }
        listItem.Add(itemToAdd);
        callbackRefreshInventory?.Invoke();
    }
    public void RemoveFromInventory(ItemData itemToRemove)
    {
        if (!listItem.Contains(itemToRemove))
        {
            return;
        }

        listItem.Remove(itemToRemove);
        callbackRefreshInventory.Invoke();
    }

    // Private Method/Function :

    private void CreateInstance()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
