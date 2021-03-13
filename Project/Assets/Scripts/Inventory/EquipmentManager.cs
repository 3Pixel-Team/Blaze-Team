using UnityEngine;
using System.Collections.Generic;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance;

    public List<Item_SO> equipmentItems = new List<Item_SO>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("[Equipment Manager] There is more than 1 instance of the equipment manager!");
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// get item equipped in the slot
    /// </summary>
    public Item_SO GetEquipment(EquipmentType _type){
        for (int i = 0; i < equipmentItems.Count; i++)
        {
            if(equipmentItems[i].equipmentType == _type){
                return equipmentItems[i];
            }
        }
        return null;
    }

    /// <summary>
    /// Equip Item
    /// </summary>
    public void EquipItem(Item_SO item)
    {
        equipmentItems.Add(item);
        IncreaseStat(item.itemType, item.itemAmount);
    }

    /// <summary>
    /// Unequip Item
    /// </summary>
    public void Unequip(Item_SO item)
    {
        equipmentItems.Remove(GetEquipment(item.equipmentType));
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