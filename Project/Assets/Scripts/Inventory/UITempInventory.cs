using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UITempInventory : MonoBehaviour
{
    public Transform inventoryParent;
    public GameObject inventoryPrefab;

    public void InitInventory(){
        Inventory();
    }

    void Inventory(){
        //clear parent
        for (int i = inventoryParent.childCount-1; i >= 0; i--)
        {
            DestroyImmediate(inventoryParent.GetChild(i).gameObject);
        }

        foreach (var item in InventoryManager.Instance.tempItems)
        {
            UIItem uiItem = Instantiate(inventoryPrefab, inventoryParent).GetComponent<UIItem>();
            uiItem.InitInventoryItem(item);
        }
        for (int i = inventoryParent.childCount; i < 24; i++)
        {
            UIItem uiItem = Instantiate(inventoryPrefab, inventoryParent).GetComponent<UIItem>();
            uiItem.InitInventoryItem(null);
        }
    }
}
