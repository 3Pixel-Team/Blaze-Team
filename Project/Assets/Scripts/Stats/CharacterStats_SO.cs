using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Character Stats", menuName ="Character Stats/Stats")]
public class CharacterStats_SO : ScriptableObject
{
    [Header("Name")]
    public string NpcName;

    [Header("Level")]
    public int level = 1;
    public int currentExp = 0;
    public int maxExp = 100;
    public float expMaxlvlMultiplier = 1.25f; //how much the maxExp will increase on each level
    public int expGivenOnDeath;

    [Header("Status Stats")]
    public int maxHealth;
    public int maxShield;
    public int currentShield;

    [Header("Attributes")]
    public int baseArmor;
    public int currentArmor;
    public int baseDamage;
    public int currentDamage;
    public float criticalChance;
    public float criticalMultiplier;
    public float speed;

    [Header("Inventory")]
    public int currentCredit;
}
