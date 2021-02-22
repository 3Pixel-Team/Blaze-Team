using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreItem : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI itemNameText;
    Item_SO item;
    bool selling;

    public void Init(Item_SO _item, bool _selling){
        item = _item;
        selling = _selling;

        if(item != null){
            iconImage.gameObject.SetActive(true);
            iconImage.sprite = item.itemSprite;
            itemNameText.gameObject.SetActive(true);
            itemNameText.text = item.itemName;
        }else
        {
            iconImage.gameObject.SetActive(false);
            itemNameText.gameObject.SetActive(false);
        }
    }

    //button
    public void Click(){
        StoreManager.Instance.store.OpenDesc(item, selling);
    }
}
