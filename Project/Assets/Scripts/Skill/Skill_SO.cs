using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "New Skill")]
public class Skill_SO : ScriptableObject
{
    public string id;
    public string skillName;
    public Sprite skillIcon;
    public int cost;
    public int coolDownTime;
    public SkillType skillType;
    public GameObject skillObject;
    [TextArea] public string skillDesc;
}

public enum SkillType
{
    MELEE, GUN, BODY
}
