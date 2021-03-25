using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIItem : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI amountText;
    Item_SO item;

    public void InitInventoryItem(Item_SO _item){
        item = _item;
        amountText.gameObject.SetActive(false);
        iconImage.gameObject.SetActive(false);
        if(item != null){
            iconImage.gameObject.SetActive(true);
            iconImage.sprite = item.itemSprite;
            if(item.isStackable){
                amountText.gameObject.SetActive(true);
                amountText.text = item.stackSize.ToString();
            }
        }
    }

    //button
    public void Click(){
        if(item && MainBaseManager.Instance) MainBaseManager.Instance.inventory.inventoryDesc.OpenEquipDesc(item);
    }
}
