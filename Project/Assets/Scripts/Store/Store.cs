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
        Init();
    }

    void Init(){
        descPanel.SetActive(false);

        List<Item_SO> sellItems = InventoryManager.Instance.items;
        InitList(sellItems, sellParent, out List<StoreItem> _sellItems);
        for (int i = 0; i < _sellItems.Count; i++)
        {
            _sellItems[i].InitSell(sellItems[i]);
        }

        List<Item_SO> buyItems = GameManager.Instance.items;
        InitList(GameManager.Instance.items, buyParent, out List<StoreItem> _buyItems);
        for (int i = 0; i < _buyItems.Count; i++)
        {
            _buyItems[i].InitBuy(buyItems[i]);
        }
    }

    void InitList(List<Item_SO> list, Transform _parent, out List<StoreItem> _storeItems){
        //clear parents
        for (int i = _parent.childCount-1; i > 0; i--)
        {
            DestroyImmediate(_parent.GetChild(i).gameObject);
        }
        for (int j = _parent.GetChild(0).childCount-1; j >= 0; j--)
        {
            DestroyImmediate(_parent.GetChild(0).GetChild(j).gameObject);
        }
        int c = list.Count>12?list.Count:12;
        for (int i = 0; i < c; i++)
        {
            if(_parent.childCount-1 < i/3){
                Instantiate(_parent.GetChild(0).gameObject, _parent);
            }
        }

        //prepare item slots
        List<StoreItem> storeItems = new List<StoreItem>();
        for (int i = 0; i < _parent.childCount; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                StoreItem st = Instantiate(storeNodePrefab, _parent.GetChild(i)).GetComponent<StoreItem>();
                st.Init();
                storeItems.Add(st);
            }
        }
        for (int i = storeItems.Count-1; i >= 0; i--)
        {
            if(i >= list.Count){
                storeItems.RemoveAt(i);
            }
        }
        _storeItems = storeItems;
    }

    //called in store items button
    public void OpenBuyDesc(Item_SO _item){
        SetDesc(_item);

        sellButton.gameObject.SetActive(false);
        buyButton.gameObject.SetActive(true);
        buyCostText.text = _item.buyCost.ToString();

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(()=> BuyItem(_item));
    }

    //called in store items button
    public void OpenSellDesc(Item_SO _item){
        SetDesc(_item);

        buyButton.gameObject.SetActive(false);
        sellButton.gameObject.SetActive(true);
        sellCostText.text = _item.sellCost.ToString();

        sellButton.onClick.RemoveAllListeners();
        sellButton.onClick.AddListener(()=> SellItem(_item));
    }

    void SetDesc(Item_SO _item){
        descPanel.SetActive(true);

        titleText.text = _item.itemName;
        iconImage.sprite = _item.itemSprite;
        maxAmmoText.text = _item.maxAmmo.ToString();
        reloadTimeText.text = _item.reloadTime.ToString();
        shotPerSecText.text = _item.shotsPerSec.ToString();
        magazineSizeText.text = _item.magazineSize.ToString();
        damageText.text = _item.weaponDamage.ToString();
    }

    //called in close button
    public void CloseDesc(){
        descPanel.SetActive(false);
    }

    //button
    public void BuyItem(Item_SO _item){
        if(GameManager.Instance.playerStats.currentCredit >= _item.buyCost){
            Debug.Log("cek buy item " + _item.itemName);
            GameManager.Instance.playerStats.TakeCredit(_item.buyCost);
            _item.AddItem();
            Init();
        }
    }

    //button
    public void SellItem(Item_SO _item){
        if(GameManager.Instance.playerStats.currentCredit >= _item.sellCost){
            Debug.Log("cek sell item " + _item.itemName);
            GameManager.Instance.playerStats.GiveCredit(_item.sellCost);
            _item.RemoveItem();
            Init();
        }
    }
}
