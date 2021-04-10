using UnityEngine;
using System.Collections.Generic;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance;

    public List<string> equipmentItems{
        get{return SaveManager.Instance.playerData.equipmentItems;}
        set{SaveManager.Instance.playerData.equipmentItems = value;}
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("[EquipmentManager] There is more then one inventory Instance");
            return;
        }
        Instance = this;
    }

    void Start(){
        SyncData();
    }

    /// <summary>
    /// Make sure the data between player data and scriptable object match
    /// </summary>
    void SyncData(){
        //remove item from player data if no match id from item list
        for (int i = equipmentItems.Count-1; i >= 0; i--)
        {
            if(InventoryManager.Instance.items.ContainsKey(equipmentItems[i]) == false){
                equipmentItems.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// get all equipped items
    /// </summary>
    public List<Item_SO> EquippedItems(){
        List<Item_SO> temps = new List<Item_SO>();
        foreach (var id in equipmentItems)
        {
            temps.Add(InventoryManager.Instance.items[id]);
        }
        return temps;
    }

    /// <summary>
    /// get item equipped in the slot
    /// </summary>
    public Item_SO GetEquipment(EquipmentType _type){
        for (int i = 0; i < equipmentItems.Count; i++)
        {
            Item_SO item = InventoryManager.Instance.items[equipmentItems[i]];
            if(item.equipmentType == _type){
                return item;
            }
        }
        return null;
    }

    /// <summary>
    /// Equip Item
    /// </summary>
    public void EquipItem(Item_SO item)
    {
        equipmentItems.Add(item.id);
        IncreaseStat(item.itemType, item.itemAmount);
    }

    /// <summary>
    /// Unequip Item
    /// </summary>
    public void Unequip(Item_SO item)
    {
        equipmentItems.Remove(item.id);
        DecreaseStat(item.itemType, item.itemAmount);
    }

    public void IncreaseStat(ItemType itemType, int amount)
    {
        if (itemType == ItemType.ARMOR)
        {
            GameManager.Instance.playerStats.IncreaseArmour(amount);

        }
        else if (itemType == ItemType.WEAPON)
        {
            GameManager.Instance.playerStats.IncreaseDamage(amount);
        }
    }

    public void DecreaseStat(ItemType itemType, int amount)
    {
        if (itemType == ItemType.ARMOR)
        {
            GameManager.Instance.playerStats.DecreaseArmour(amount);
        }
        else if (itemType == ItemType.WEAPON)
        {
            GameManager.Instance.playerStats.DecreaseDamage(amount);
        }
    }
}