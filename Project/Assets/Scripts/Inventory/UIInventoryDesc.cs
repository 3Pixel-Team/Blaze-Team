using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInventoryDesc : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public Image iconImage;
    public TextMeshProUGUI maxAmmoText;
    public TextMeshProUGUI reloadTimeText;
    public TextMeshProUGUI shotPerSecText;
    public TextMeshProUGUI magazineSizeText;
    public TextMeshProUGUI damageText;
    public Button equipButton, unequipButton;

    public void OpenUnequipDesc(Item_SO _item){
        SetDesc(_item);

        equipButton.gameObject.SetActive(false);
        unequipButton.gameObject.SetActive(true);

        unequipButton.onClick.RemoveAllListeners();
        unequipButton.onClick.AddListener(()=> UnequipItem(_item));
    }

    public void OpenEquipDesc(Item_SO _item){
        SetDesc(_item);

        unequipButton.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(true);

        equipButton.onClick.RemoveAllListeners();
        equipButton.onClick.AddListener(()=> EquipItem(_item));
    }

    /// <summary>
    /// set item data interface
    /// </summary>
    void SetDesc(Item_SO _item){
        gameObject.SetActive(true);

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
        gameObject.SetActive(false);
    }

    //button
    public void UnequipItem(Item_SO _item){
        MainBaseManager.Instance.inventory.UnequipItem(_item);
    }

    //button
    public void EquipItem(Item_SO _item){
        MainBaseManager.Instance.inventory.EquipItem(_item);
    }
}
