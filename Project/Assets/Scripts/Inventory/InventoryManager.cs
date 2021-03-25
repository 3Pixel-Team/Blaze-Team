using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<Item_SO> items = new List<Item_SO>();
    public List<Item_SO> currentItems = new List<Item_SO>();
    public List<Item_SO> tempItems = new List<Item_SO>();


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("[InventoryManager] There is more then one inventory Instance");
            return;
        }
        Instance = this;

        DontDestroyOnLoad(this.gameObject);

        LoadResources();
    }

    /// <summary>
    /// Load All items in Resources foldes
    /// </summary>
    void LoadResources(){
        items = new List<Item_SO>();
        items = Resources.LoadAll<Item_SO>("Items").ToList();
    }

    /// <summary>
    /// get all item for the store
    /// </summary>
    public List<Item_SO> ItemStore(){
        List<Item_SO> temps = new List<Item_SO>();
        foreach (var item in items)
        {
            if(item.itemType == ItemType.ARMOR || item.itemType == ItemType.WEAPON){
                temps.Add(item);
            }
        }
        return temps;
    }

    /// <summary>
    /// Add an item to the inventory
    /// </summary>
    public void AddItemToInventory(Item_SO _item){
        if(_item.isStackable){
            for (int i = 0; i < currentItems.Count; i++)
            {
                if(currentItems[i] == _item){
                    currentItems[i].stackSize += _item.stackSize;
                    return;
                }
            }
        }
        currentItems.Add(_item);
    }

    /// <summary>
    /// remove an item from the inventory
    /// </summary>
    public void RemoveItemFromInventory(Item_SO _item){
        if(!currentItems.Contains(_item)) return;
        if(_item.isStackable){
            for (int i = 0; i < currentItems.Count; i++)
            {
                if(currentItems[i] == _item){
                    currentItems[i].stackSize -= _item.stackSize;
                    return;
                }
            }
            currentItems.Remove(_item);
        }else
        {
            currentItems.Remove(_item);
        }
    }

    /// <summary>
    /// Get all items in temporary inventory and move them to inventory
    /// </summary>
    public void TransferTempToInventory(){
        for (int i = 0; i < tempItems.Count; i++)
        {
            AddItemToInventory(tempItems[i]);
        }
        tempItems.Clear();
    }

    /// <summary>
    /// Add an item to the temporary inventory
    /// </summary>
    public void AddItemToTemp(Item_SO _item, out bool picked){
        switch (_item.itemType)
        {
            case ItemType.HEALTH:
                _item.UseItem();
                picked = true;
            break;
            case ItemType.ARMOR:
                tempItems.Add(_item);
                picked = true;
            break;
            case ItemType.WEAPON:
                tempItems.Add(_item);
                picked = true;
            break;
            case ItemType.AMMO:
                _item.UseItem();
                picked = true;
            break;
            default:
                picked = false;
            break;
        }
    }
}