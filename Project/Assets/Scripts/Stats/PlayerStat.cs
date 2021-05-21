using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : CharacterStats
{
    public Player_SO playerStat;

    public int currentLevel;
    public int currentExp;
    public int currentCredit;

    PlayerStatManager stat => PlayerStatManager.Instance;

    public override void InitCharacterStat()
    {
        if (playerStat == null) Debug.LogError("playerStat is null");

        currentHealth = (int)stat.GetTotalAttributeLevel(TypeOfAttributes.HEALTH);
        currentShield = (int)stat.GetTotalAttributeLevel(TypeOfAttributes.SHIELD);
        currentDefense = (int)stat.GetTotalAttributeLevel(TypeOfAttributes.DEFENCE);
        currentAttackPower = (int)stat.GetTotalAttributeLevel(TypeOfAttributes.ATTACK);
        critChance = (int)stat.GetTotalAttributeLevel(TypeOfAttributes.CRITCHANCE);
    }

    public override void TakeDamage(int amount)
    {
        currentShield -= amount;
        int diff = Mathf.Abs(currentShield);
        if (currentShield < 0) currentShield = 0;
        base.TakeDamage(diff);
    }

    public override int GetMaxHealth()
    {
        return (int)stat.GetAttributeLevel(TypeOfAttributes.HEALTH, out bool maxed);
    }

    public void GiveHealth(int amount)
    {
        currentHealth += amount;
        int maxHealth = (int)stat.GetAttributeLevel(TypeOfAttributes.HEALTH, out bool maxed);
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void GiveShield(int amount)
    {
        currentShield += amount;
        int maxShield = (int)stat.GetAttributeLevel(TypeOfAttributes.SHIELD, out bool maxed);
        if (currentShield > maxShield)
        {
            currentShield = maxShield;
        }
    }

    public void GiveCredit(int amount)
    {
        currentCredit += amount;
    }

    public void LevelUpStatsChange()
    {
        currentHealth = (int)stat.GetAttributeLevel(TypeOfAttributes.HEALTH, out _);
        currentShield = (int)stat.GetAttributeLevel(TypeOfAttributes.SHIELD, out _);
        currentDefense = (int)stat.GetAttributeLevel(TypeOfAttributes.DEFENCE, out _);
    }

    public int MaxExp()
    {
        return stat.MaxCurrentLevelExp(currentLevel);
    }

    public void GiveExp(int amount)
    {
        currentExp += amount;
        if (currentExp >= MaxExp())
        {
            while (currentExp >= MaxExp()) //level up
            {
                currentExp -= MaxExp();
                currentLevel++;
                PlayerManager.Instance.LevelUpEventCall();
            }
        }
        else
        {
            PlayerManager.Instance.ExpChangeEventCall();
        }
    }
}
