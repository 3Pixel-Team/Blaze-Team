using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public static StoreManager Instance;

    public Store store;

    void Awake(){
        if (Instance != null)
        {
            Debug.Log("[StoreManager] There is more then one Store Instance");
            return;
        }
        Instance = this;
    }
}
