﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBaseManager : MonoBehaviour
{
    public static MainBaseManager Instance;
    public UIBaseInventory inventory;
    public UIStore store;
    public GameObject stats;
    public GameObject mission;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("[InventoryManager] There is more then one inventory Instance");
            return;
        }
        Instance = this;
    }

    void Start(){
        inventory.InitInventory();
        store.InitStore();
    }

    //open store
    public void StoreToggle(bool value){
        if(value){
            store.gameObject.SetActive(true);
            store.InitStore();
        }else
        {
            store.gameObject.SetActive(false);
        }
    }

    //open inventory
    public void InventoryToggle(bool value){
        if(value){
            inventory.gameObject.SetActive(true);
            inventory.InitInventory();
        }else
        {
            inventory.gameObject.SetActive(false);
        }
    }

    //open stats
    public void StatToggle(bool value){
        if(value){
            stats.gameObject.SetActive(true);
        }else
        {
            stats.gameObject.SetActive(false);
        }
    }

    //open mission
    public void MissionToggle(bool value){
        if(value){
            mission.gameObject.SetActive(true);
        }else
        {
            mission.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Go to game
    /// </summary>
    public void ChangeScenePlay()
    {
        GameManager.Instance.GoToGameScene();
    }
}
