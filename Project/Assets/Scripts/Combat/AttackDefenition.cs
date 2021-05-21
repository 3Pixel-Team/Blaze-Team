using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Attack.asset", menuName ="Attack/New Attack")]
public class AttackDefenition : ScriptableObject
{
    public float coolDown = 1;

    public float range;

    public float criticalMultiplier = 2;
    public float criticalChance = 1;

    public Attack CreateAttack (CharacterStats attacker, CharacterStats defender)
    {
        float baseDamage = attacker.currentAttackPower;
        float critChance = criticalChance + attacker.critChance;

        bool isCritical = Random.value < critChance;
        if (isCritical)
        {
            baseDamage = baseDamage * criticalMultiplier;
        }
        if(defender != null)
        {
            float defenderArmor = defender.currentDefense;
            if(defenderArmor >= baseDamage)
            {
                baseDamage = 1;
            }
            else
            {
                baseDamage -= defenderArmor;
            }
        }

        return new Attack((int)baseDamage, isCritical);
    }

    public int ExecuteAttack(GameObject attacker, GameObject target)
    {
        if (target == null)
            return 0;

        // check if target is in range of player
        if (Vector3.Distance(attacker.transform.position, target.transform.position) > range)
            return 0;

        // check if target is in front of the player
        if (!attacker.transform.IsFacingTarget(target.transform))
            return 0;

        // at this point the attack will connect
        var attack = CreateAttack(attacker.GetComponent<CharacterStats>(), target.GetComponent<CharacterStats>());

        var attackables = target.GetComponents<IAttackable>();

        foreach (var a in attackables)
        {
            ((IAttackable)a).OnAttack(attacker.gameObject, attack);
        }

        return attack.Damage;
    }
}
