using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    #region Singleton

    public static InventoryManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("[InventoryManager] There is more then one inventory Instance");
            return;
        }
        Instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    #endregion

    public static readonly int maxSlots = 12;
    [SerializeField] private int emptySlots = 12;

    #region Events

    // whenever a change in the items occur, call this function
    public delegate void OnChangedItem();
    public OnChangedItem onChangedItemCall;

    #endregion

    public Component inventoryCache = null;
    [SerializeField] private GameObject inventoryUI = null;
    [SerializeField] private InventorySlot[] inventorySlots;

    [SerializeField]
    public List<Item_SO> items = new List<Item_SO>();

    private void Start()
    {
        onChangedItemCall += UpdateInventorySlots;
        GameManager.Instance.OnLevelStartedEvent += InitiateInventoryCache;
        GameManager.Instance.OnLevelLostEvent += ClearInventoryCache;
        GameManager.Instance.OnLevelWonEvent += GetInventoryFromCache;



        LoadInventory();
    }

    public void LoadInventory()
    {

        inventorySlots = inventoryUI.GetComponentsInChildren<InventorySlot>();
        if (items.Count != 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                inventorySlots[i].AddItemToSlot(items[i]);
            }
        }
        onChangedItemCall?.Invoke();
    }

    #region Item Handling

    public bool AddItem(Item_SO item)
    {
        if (emptySlots > 0)
        {
            foreach (InventorySlot slot in inventorySlots)
            {
                if (slot.isEmptySlot)
                {
                    slot.AddItemToSlot(item);
                    items.Add(item);
                    Debug.Log("[Inventory Manager] Item added to inventory");
                    break;
                }
            }
        }
        else
        {
            Debug.Log("[InventoryManager - AddItem] Inventory is full.");
            return false;
        }

        onChangedItemCall?.Invoke();
        item.stackSize++;
        return true;
    }

    public void RemoveItem(Item_SO item)
    {
        items.Remove(item);
        item.RemoveItem();

        onChangedItemCall?.Invoke();
    }

    public List<InventorySlot> NotEmptySlots(){
        List<InventorySlot> slots = new List<InventorySlot>();
        foreach (var slot in inventorySlots)
        {
            if(slot.Item != null){
                slots.Add(slot);
            }
        }
        return slots;
    }

    #endregion

    #region Updating UI

    public void UpdateInventorySlots()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if(i < items.Count){
                inventorySlots[i].AddItemToSlot(items[i]);
                emptySlots--;
            }else
            {
                inventorySlots[i].ClearSlot();
                emptySlots++;
            }

            // if (inventorySlots[i].Item == null)
            // {
            //     inventorySlots[i].ClearSlot();
            //     emptySlots++;
            // }
            // else
            // {
            //     inventorySlots[i].AddItemToSlot(items[i]);
            //     emptySlots--;
            // }
        }
    }

    public void ToggleInventory()
    {
        onChangedItemCall?.Invoke();
        if (inventoryUI.activeSelf == true)
        {
            inventoryUI.SetActive(false);
        }
        else
        {
            inventoryUI.SetActive(true);
        }
    }

    public bool AddItemToInventory(Item_SO item)
    {
        bool itemAdded = false;
        if (item.isStackable)
        {
            foreach (InventorySlot slot in inventorySlots)
            {
                InventorySlot tmpSlot = slot.GetComponent<InventorySlot>();
                var itemTmp = tmpSlot.Item;
                if (itemTmp != null && itemTmp.itemType == item.itemType && itemTmp.stackSize < item.maxStackSize)
                {
                    tmpSlot.AddItemToSlot(item);
                    itemTmp.stackSize++;
                    Debug.Log("[Inventory Manager] Item added to the stack of " + item.itemName);
                    itemAdded = true;
                    break;
                }
            }

            if (itemAdded)
            {
                onChangedItemCall?.Invoke();
                return true;
            }
        }

        if (emptySlots > 0)
        {
            return AddItem(item);
        }
        else
        {
            Debug.Log("[Inventory Manager] inventory is full");
        }

        return false;
    }

    public void RemoveItemFromInventory(Item_SO item)
    {
        if (item.stackSize == 0 || !item.isStackable)
        {
            items.Remove(item);
        }
        else
        {
            Debug.LogWarning("[Inventory Manager] There is a call to remove item with more than one stack size");
        }

        onChangedItemCall?.Invoke();
    }

    #endregion


    #region Temporary level inventory

    public void InitiateInventoryCache()
    {
        inventoryCache = gameObject.AddComponent(typeof(InventoryCache));
    }

    public void ClearInventoryCache()
    {
        Destroy(inventoryCache);
    }
    
    public void GetInventoryFromCache()
    {
        // todo

        Destroy(inventoryCache);
    }

    #endregion
}