using System;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    #region Defenitions

    public CharacterStats_SO stats_Template;
    public CharacterStats_SO stats;


    public bool isPlayer = false;

    #endregion

    private void Start()
    {
        if (isPlayer == true)
        {
            stats_Template = GameManager.Instance.playerStats;
        }
        else if (GetComponent<PlayerManager>())
        {
            isPlayer = true;
            stats_Template = GameManager.Instance.playerStats;
        }

        if (stats_Template != null)
        {
            stats = Instantiate(stats_Template);
        }
        else
        {
            Debug.Log("[Character Stats] There is no stats template attached to " + gameObject.name);
        }
    }

    #region Increasers
    public void GiveHealth(int amount)
    {
        stats.GiveHealth(amount);
    }
    public void GiveShield(int amount)
    {
        stats.GiveShield(amount);
    }
    public void GiveCredit(int amount)
    {
        stats.GiveCredit(amount);
    }

    public void GiveExp(int amount)
    {
        stats.GiveExp(amount);
    }

    public void LevelUpStatsChange()
    {
        stats.LevelUpStatsChange();
    }

    #endregion

    #region Decreasers
    public void TakeDamage(int amount)
    {
        if(stats.currentShield > 0)
        {
            LoseShield(amount);
        }
        else
        {
            stats.TakeDamage(amount);
        }
    }
    public void LoseShield(int amount)
    {
        int leftOverAmount;

        if (stats.currentShield - amount <= 0)
        {
            leftOverAmount = amount - stats.currentShield;
            stats.LoseShield(stats.currentShield);
            stats.TakeDamage(leftOverAmount);
        }
        else
        {
            stats.LoseShield(amount);
        }
    }
    public void TakeCredit(int amount)
    {
        stats.TakeCredit(amount);
        //update inventory event
    }
    #endregion

    #region Getters

    public int GetHealth()
    {
        return stats.currentHealth;
    }
    public int GetMaxHealth()
    {
        return stats.maxHealth;
    }
    public int GetShield()
    {
        return stats.currentShield;
    }
    public int GetMaxShield()
    {
        return stats.maxShield;
    }

    public int GetDamage()
    {
        return stats.currentDamage;
    }
    public int GetArmor()
    {
        return stats.currentArmor;
    }
    
    public float GetCriticalChance()
    {
        return stats.criticalChance;
    }

    public int GetLevel()
    {
        return stats.level;
    }

    public int GetActualExp()
    {
        return stats.currentExp;
    }

    public int GetMaxExp()
    {
        return stats.maxExp;
    }

    public int GetExpOnDeath()
    {
        return stats.expGivenOnDeath;
    }
    #endregion

    #region Save and Load

    public void SaveStats()
    {
        stats_Template = stats;
        stats_Template.SaveStats();
    }

    public void LoadStats()
    {
        stats_Template.LoadStats();
    }
    
    #endregion
}
