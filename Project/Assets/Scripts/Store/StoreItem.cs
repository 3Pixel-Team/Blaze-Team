using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreItem : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI itemNameText;
    public Button clickButton;

    public void Init(){
        iconImage.gameObject.SetActive(false);
        itemNameText.gameObject.SetActive(false);
    }

    public void InitBuy(Item_SO _item){
        iconImage.gameObject.SetActive(true);
        itemNameText.gameObject.SetActive(true);
        
        iconImage.sprite = _item.itemSprite;
        itemNameText.text = _item.itemName;

        clickButton.onClick.RemoveAllListeners();
        clickButton.onClick.AddListener(()=>{
            StoreManager.Instance.store.OpenBuyDesc(_item);
        });
    }

    public void InitSell(Item_SO _item){
        iconImage.gameObject.SetActive(true);
        itemNameText.gameObject.SetActive(true);

        iconImage.sprite = _item.itemSprite;
        itemNameText.text = _item.itemName;

        clickButton.onClick.RemoveAllListeners();
        clickButton.onClick.AddListener(()=>{
            StoreManager.Instance.store.OpenSellDesc(_item);
        });
    }
}
