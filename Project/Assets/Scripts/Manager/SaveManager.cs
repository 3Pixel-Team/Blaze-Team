using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public PlayerData playerData;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("[SaveManager] There is more then one Instance");
            return;
        }
        Instance = this;
        Load();
    }

    /// <summary>
    /// Save Data
    /// </summary>
    [ContextMenu("Save Data")]
    public void Save()
    {
        BinaryFormatter bin = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/PlayerInfo.dat");

        PlayerData data = playerData;

        Debug.Log("save");

        bin.Serialize(file, data);
        file.Close();
    }

    /// <summary>
    /// Load Data
    /// </summary>
    [ContextMenu("Load Data")]
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerInfo.dat"))
        {
            BinaryFormatter bin = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/PlayerInfo.dat", FileMode.Open);

            PlayerData data = (PlayerData)bin.Deserialize(file);
            file.Close();

            Debug.Log("Load");

            playerData = data;
        }else
        {
            playerData = new PlayerData();
        }
    }

    /// <summary>
    /// Delete Data
    /// </summary>
    [ContextMenu("Delete Data")]
    public void Delete()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerInfo.dat"))
        {
            File.Delete(Application.persistentDataPath + "/PlayerInfo.dat");
        }

        PlayerPrefs.DeleteAll();
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}

[System.Serializable]
public class PlayerData
{
    [Header("Player")]
    public int level;
    public int maxShield;
    public int maxHealth;
    public int maxExp;
    public int currentExp;
    public int currentCredit;
    public int baseArmor;
    public float criticalChance;
    public int baseDamage;

    [Header("Inventory")]
    public List<string> inventoryItems = new List<string>();
    public List<string> equipmentItems = new List<string>();

    [Header("Skill")]
    public int skillPoint = 5;
    public SkillData meleeSkill = new SkillData();
    public SkillData gunSkill = new SkillData();
    public SkillData bodySkill = new SkillData();
}

[System.Serializable]
public class SkillData
{
    public int level = 0;
    public string equipped = string.Empty;
}
