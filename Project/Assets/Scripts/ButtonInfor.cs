using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfor : MonoBehaviour
{
    public int ItemID;
    public Text PriceTxt;
    public Text QuantityTxt;
    public GameObject shopManager;

    void Update()
    {
        PriceTxt.text = "Price : $ " + shopManager.GetComponent<ShopManager>().shopItems[2, ItemID].ToString();
        QuantityTxt.text = shopManager.GetComponent<ShopManager>().shopItems[3, ItemID].ToString();
    }
}
