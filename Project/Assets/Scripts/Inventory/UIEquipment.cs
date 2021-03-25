using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIEquipment : MonoBehaviour
{
    public EquipmentType equipmentType;
    public Image iconImage;
    public GameObject placeHolderImage;
    Item_SO item;

    public void InitEquipmentItem(Item_SO _item){
        item = _item;
        placeHolderImage.SetActive(true);
        iconImage.gameObject.SetActive(false);
        if(item != null){
            placeHolderImage.SetActive(false);
            iconImage.gameObject.SetActive(true);
            iconImage.sprite = item.itemSprite;
        }
    }

    //button
    public void Click(){
        if(item!=null) MainBaseManager.Instance.inventory.inventoryDesc.OpenUnequipDesc(item);
    }
}
