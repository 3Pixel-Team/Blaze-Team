using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item_SO Item;

    public Image icon;

    public Text itemAmount;

    public bool isEmptySlot = true;

    public void AddItemToSlot(Item_SO newItem)
    {
        if (Item != null)
        {
            if (Item.isStackable)
            {
                UpdateStackSize();
            }
            return;
        }

        Item = newItem;
        icon.sprite = Item.itemSprite;
        icon.enabled = true;
        //UpdateStackSize();
        isEmptySlot = false;
    }

    public void ClearSlot()
    {
        Item = null;
        icon.sprite = null;
        icon.enabled = false;
        if (itemAmount)
        {
            itemAmount.text = "0";
            itemAmount.enabled = false;
        }
        isEmptySlot = true;
    }

    public void UseItem()
    {
        if(Item != null)
        {
            Item_SO tempItem = Item;

            if (Item.stackSize <= 1 || !Item.isStackable)
            {
                ClearSlot();
            }
            tempItem.UseItem();

            UpdateStackSize();
        }

    }

    public void UnequipItem()
    {
        // if(Item != null)
        // {
        //     EquipmentManager.Instance.Unequip(Item, (int)Item.equipmentType);
        // }
    }

    public void UpdateStackSize()
    {
        if(Item == null)
        {
            return;
        }
        if (Item.isStackable == true && Item.stackSize >= 1)
        {
            itemAmount.text = Item.stackSize.ToString();
            itemAmount.enabled = true;
        }
    }
}