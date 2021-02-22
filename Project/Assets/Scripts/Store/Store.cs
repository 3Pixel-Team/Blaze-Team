using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Store : MonoBehaviour
{
    public GameObject storeNodePrefab;
    public Transform sellParent;
    public Transform buyParent;

    [Header("Store Desc")]
    public GameObject descPanel;
    public TextMeshProUGUI titleText;
    public Image iconImage;
    public TextMeshProUGUI maxAmmoText;
    public TextMeshProUGUI reloadTimeText;
    public TextMeshProUGUI shotPerSecText;
    public TextMeshProUGUI magazineSizeText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI sellCostText;
    public TextMeshProUGUI buyCostText;
    public Button sellButton, buyButton;

    void Start(){
        List<Item_SO> items = GameManager.Instance.items;

        //only get weapons
        for (int j = items.Count-1; j >= 0; j--)
        {
            if(items[j].itemType != ItemType.WEAPON){
                items.RemoveAt(j);
            }
        }

        InitItems(items, sellParent, true);
        InitItems(items, buyParent, false);

        descPanel.SetActive(false);
    }

    void InitItems(List<Item_SO> items, Transform storeParent, bool selling){
        //clear parents
        for (int i = 0; i < items.Count; i++)
        {
            if(storeParent.childCount-1 < i/3){
                Instantiate(storeParent.GetChild(0).gameObject, storeParent);
            }
            Transform parent = storeParent.GetChild(i/3);
            for (int j = parent.childCount-1; j >= 0; j--)
            {
                DestroyImmediate(parent.GetChild(j).gameObject);
            }
        }

        //prepare buy item slots
        List<StoreItem> storeItems = new List<StoreItem>();
        for (int i = 0; i < storeParent.childCount; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                StoreItem st = Instantiate(storeNodePrefab, storeParent.GetChild(i)).GetComponent<StoreItem>();
                st.Init(null, selling);
                storeItems.Add(st);
            }
        }
        //init buy items
        for (int i = 0; i < storeItems.Count; i++)
        {
            if(i < items.Count){
                storeItems[i].Init(items[i], selling);
            }
        }
    }

    //called in store items button
    public void OpenDesc(Item_SO _item, bool selling){
        descPanel.SetActive(true);

        titleText.text = _item.itemName;
        iconImage.sprite = _item.itemSprite;
        maxAmmoText.text = _item.maxAmmo.ToString();
        reloadTimeText.text = _item.reloadTime.ToString();
        shotPerSecText.text = _item.shotsPerSec.ToString();
        magazineSizeText.text = _item.magazineSize.ToString();
        damageText.text = _item.weaponDamage.ToString();

        sellButton.gameObject.SetActive(false);
        buyButton.gameObject.SetActive(false);
        if(selling){
            sellButton.gameObject.SetActive(true);
            sellCostText.text = _item.sellCost.ToString();

            sellButton.onClick.RemoveAllListeners();
            sellButton.onClick.AddListener(()=> SellItem(_item));
        }else
        {
            buyButton.gameObject.SetActive(true);
            buyCostText.text = _item.buyCost.ToString();

            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(()=> BuyItem(_item));
        }
    }

    //called in close button
    public void CloseDesc(){
        descPanel.SetActive(false);
    }

    //button
    public void BuyItem(Item_SO _item){
        
    }

    //button
    public void SellItem(Item_SO _item){

    }
}
