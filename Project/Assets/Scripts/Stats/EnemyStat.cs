using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : CharacterStats
{
    public Enemy_SO enemyStat;

    public override void InitCharacterStat()
    {
        if (enemyStat == null) Debug.LogError("enemystat is null");

        currentHealth = enemyStat.health;
        currentShield = enemyStat.shield;
        currentDefense = enemyStat.defense;
        currentAttackPower = enemyStat.attack;
        critChance = enemyStat.criticalChance;
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
    }

    public override int GetMaxHealth()
    {
        return enemyStat.health;
    }
}
