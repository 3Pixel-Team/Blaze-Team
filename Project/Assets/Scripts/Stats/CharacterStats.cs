using System;
using UnityEngine;

public abstract class CharacterStats : MonoBehaviour
{
    public int currentHealth;
    public int currentShield;
    public int currentDefense;
    public int currentAttackPower;
    public float critChance;

    public abstract void InitCharacterStat();

    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("character dead", gameObject);
        }
    }

    public abstract int GetMaxHealth();
}
