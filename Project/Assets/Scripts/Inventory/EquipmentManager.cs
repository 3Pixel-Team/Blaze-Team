using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    #region Singleton

    public static EquipmentManager Instance;

    private void Awake()
    {

        if (Instance != null)
        {
            Debug.Log("[Equipment Manager] There is more than 1 instance of the equipment manager!");
        }
        else
        {
            Instance = this;
        }
    }

    #endregion

    public delegate void OnEquipmentChanged(Item_SO newItem, Item_SO oldItem);
    public OnEquipmentChanged onEquipmentChanged;
    #region References

    private Item_SO[] currentEquipment;


    public GameObject equipmentUI;

    public Item_SO UiTestSimulationItemInsert;

    public InventorySlot[] equipmentSlots;

    #endregion


    private void Start()
    {
        if (equipmentUI.activeSelf == true)
        {
            equipmentUI.SetActive(false);
        }

        //setting up the equipment slots
        int equipmentSlots = System.Enum.GetNames(typeof(EquipmentType)).Length;
        currentEquipment = new Item_SO[equipmentSlots];

        DontDestroyOnLoad(gameObject);

        if (UiTestSimulationItemInsert)
        {
            if (UiTestSimulationItemInsert.itemType == ItemType.ARMOR)
                EquipItem(UiTestSimulationItemInsert);
            if (UiTestSimulationItemInsert.itemType == ItemType.WEAPON)
                EquipItem(UiTestSimulationItemInsert);
            UiTestSimulationItemInsert = null;
        }

        LoadEquipment();
    }

    public void LoadEquipment()
    {
        equipmentSlots = equipmentUI.GetComponentsInChildren<InventorySlot>();
        UpdateEquipment();
    }

    public void UpdateEquipment()
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (currentEquipment[i] == null)
            {
                equipmentSlots[i].ClearSlot();
            }
            else
            {
                equipmentSlots[i].AddItemToSlot(currentEquipment[i]);
            }
        }
    }

    public void EquipItem(Item_SO item)
    {
        int slotIndex = (int)item.equipmentType;

        Item_SO oldItem = null;
        item.RemoveItem(item);
        //if equipment slot is full, unequip the old item and add it to inventory
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            Unequip(oldItem, slotIndex);    
        }

        //set the current equipped item to the equipment slot
        currentEquipment[slotIndex] = item;

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(item, oldItem);
        }


        switch (item.itemType)
        {
            case ItemType.ARMOR:
                IncreaseStat(item.itemType, item.itemAmount);
                break;

            case ItemType.WEAPON:
                IncreaseStat(item.itemType, item.itemAmount);
                break;

            default:
                print("no other item type found");
                break;
        }
        item.RemoveItem(item);
        UpdateEquipment();
    }

    #region Unequipping

    public void Unequip(Item_SO item, int slot)
    {
        if(currentEquipment[slot] != null)
        {
            equipmentSlots[slot].ClearSlot();
            currentEquipment[slot] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, item);
            }

            switch (item.itemType)
            {
                case ItemType.ARMOR:
                    DecreaseStat(item.itemType, item.itemAmount);
                    break;

                case ItemType.WEAPON:
                    DecreaseStat(item.itemType, item.itemAmount);
                    break;

                default:
                    print("no other item type found");
                    break;
            }
            item.AddItem(item);
        }
        else
        {
            Debug.Log("[Equipment Manager] There is no item to Unequip!");
        }
        UpdateEquipment();
    }

    #endregion 

    #region StatsChangers

    public void IncreaseStat(ItemType itemType, int amount)
    {
        if (itemType == ItemType.ARMOR)
        {
            GameManager.Instance.playerStats.IncreaseArmour(amount);

        }
        else if (itemType == ItemType.WEAPON)
        {
            GameManager.Instance.playerStats.IncreaseDamage(amount);
        }
    }

    public void DecreaseStat(ItemType itemType, int amount)
    {
        if (itemType == ItemType.ARMOR)
        {
            GameManager.Instance.playerStats.DecreaseArmour(amount);
        }
        else if (itemType == ItemType.WEAPON)
        {
            GameManager.Instance.playerStats.DecreaseDamage(amount);
        }
    }

    #endregion StatsChangers

    public void ToggleEquipment()
    {
        if (equipmentUI.activeInHierarchy)
            equipmentUI.SetActive(false);
        else
            equipmentUI.SetActive(true);
    }
}