using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{
    public int[,] shopItems = new int[5, 5];
    public float coins;
    public Text CoinsTxt;
    public float storeValue;
    public GameObject Buybutton;
    
    
    void Start()
    {
        CoinsTxt.text = "Coins :" + coins.ToString();
    
        //ID
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;
        

        //Price
        shopItems[2, 1] = 50;
        shopItems[2, 2] = 100;
        shopItems[2, 3] = 150;
        shopItems[2, 4] = 400;
        

        //Qty
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
        shopItems[3, 4] = 0;

        //Store
        shopItems[4, 1] = 10;
        shopItems[4, 2] = 5;
        shopItems[4, 3] = 3;
        shopItems[4, 4] = 1;
    }

  
    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
      
        if (coins >= shopItems[2, ButtonRef.GetComponent<ButtonInfor>().ItemID])
        {
            coins -= shopItems[2, ButtonRef.GetComponent<ButtonInfor>().ItemID];
            storeValue = shopItems[4, ButtonRef.GetComponent<ButtonInfor>().ItemID]--;//  added value
            if (storeValue <= 1)
            {

                ButtonRef.SetActive(false);
            }
            shopItems[3, ButtonRef.GetComponent<ButtonInfor>().ItemID]++;
            CoinsTxt.text = " Coins : " + coins.ToString();
            ButtonRef.GetComponent<ButtonInfor>().QuantityTxt.text = shopItems[3, ButtonRef.GetComponent<ButtonInfor>().ItemID].ToString();
            
        }
        
        //}
    }

    public void Sell()
    {
        GameObject ButtonRef2 = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        coins += shopItems[2, ButtonRef2.GetComponent<ButtonInfor>().ItemID];
        storeValue = shopItems[4, ButtonRef2.GetComponent<ButtonInfor>().ItemID]++;
        shopItems[3, ButtonRef2.GetComponent<ButtonInfor>().ItemID]--;
        CoinsTxt.text = " Coins : " + coins.ToString();
        ButtonRef2.GetComponent<ButtonInfor>().QuantityTxt.text = shopItems[3, ButtonRef2.GetComponent<ButtonInfor>().ItemID].ToString();
        if (!Buybutton)
        {
            Buybutton = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
            Buybutton.SetActive(true);
        }
    }
}
