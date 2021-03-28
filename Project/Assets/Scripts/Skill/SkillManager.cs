using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;

    public Color meleeColor, gunColor, bodyColor, offColor;
    public Color meleeOffColor, gunOffColor, bodyOffColor;
    public Dictionary<string, Skill_SO> skills = new Dictionary<string, Skill_SO>();
    public List<string> meleeSkills = new List<string>();
    public List<string> gunSkills = new List<string>();
    public List<string> bodySkills = new List<string>();

    public Skill_SO meleeSkill {
        get{ return GetSkillSO(playerData.meleeSkill.equipped);}
        set{playerData.meleeSkill.equipped = value?value.id:string.Empty;}
    }
    public Skill_SO gunSkill{
        get{ return GetSkillSO(playerData.gunSkill.equipped);}
        set{playerData.gunSkill.equipped = value?value.id:string.Empty;}
    }
    public Skill_SO bodySkill{
        get{ return GetSkillSO(playerData.bodySkill.equipped);}
        set{playerData.bodySkill.equipped = value?value.id:string.Empty;}
    }

    PlayerData playerData => SaveManager.Instance.playerData;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("[SkillManager] There is more then one Instance");
            return;
        }
        Instance = this;

        DontDestroyOnLoad(this.gameObject);

        LoadResources();
    }

    /// <summary>
    /// Load All skills in Resources foldes
    /// </summary>
    void LoadResources(){
        skills = new Dictionary<string, Skill_SO>();
        List<Skill_SO> tempSkills = Resources.LoadAll<Skill_SO>("Skills").ToList();

        meleeSkills = new List<string>();
        gunSkills = new List<string>();
        bodySkills = new List<string>();
        foreach (var skill in tempSkills)
        {
            skills.Add(skill.id, skill);
            switch (skill.skillType)
            {
                case SkillType.MELEE:
                    meleeSkills.Add(skill.id);
                break;
                case SkillType.GUN:
                    gunSkills.Add(skill.id);
                break;
                case SkillType.BODY:
                    bodySkills.Add(skill.id);
                break;
            }
        }
    }

    /// <summary>
    /// Change skill data equipped
    /// </summary>
    public void EquipSkill(Skill_SO skill){
        switch (skill.skillType)
        {
            case SkillType.MELEE:
                meleeSkill = skill;
            break;
            case SkillType.GUN:
                gunSkill = skill;
            break;
            case SkillType.BODY:
                bodySkill = skill;
            break;
        }
    }

    /// <summary>
    /// Change skill data equipped
    /// </summary>
    public void UnequipSkill(Skill_SO skill){
        switch (skill.skillType)
        {
            case SkillType.MELEE:
                meleeSkill = null;
            break;
            case SkillType.GUN:
                gunSkill = null;
            break;
            case SkillType.BODY:
                bodySkill = null;
            break;
        }
    }

    /// <summary>
    /// Unlock skill
    /// </summary>
    public void UnlockSkillData(Skill_SO skill){
        switch (skill.skillType)
        {
            case SkillType.MELEE:
                playerData.meleeSkill.level++;
            break;
            case SkillType.GUN:
                playerData.gunSkill.level++;
            break;
            case SkillType.BODY:
                playerData.bodySkill.level++;
            break;
        }
    }

    /// <summary>
    /// check if the skill is already equipped
    /// </summary>
    public bool IsEquipped(Skill_SO skill){
        switch (skill.skillType)
        {
            case SkillType.MELEE:
            return (meleeSkill == skill);
            case SkillType.GUN:
            return (gunSkill == skill);
            case SkillType.BODY:
            return (bodySkill == skill);
        }
        return false;
    }

    /// <summary>
    /// get color of skill icon
    /// </summary>
    public Color GetIconColor(Skill_SO skill){
        int lockState = IsUnlocked(skill);
        if(lockState == 1 || lockState == 0){
            return Color.white;
        }else {
            return Color.black;
        }
    }

    /// <summary>
    /// get bg color of skill icon
    /// </summary>
    public Color GetBgColor(Skill_SO skill){
        int lockState = IsUnlocked(skill);
        if(lockState == 1){
            switch (skill.skillType)
            {
                case SkillType.MELEE:
                if(skill == meleeSkill){
                    return meleeColor;
                }else
                {
                    return meleeOffColor;
                }
                case SkillType.GUN:
                if(skill == gunSkill){
                    return gunColor;
                }else
                {
                    return gunOffColor;
                }
                case SkillType.BODY:
                if(skill == bodySkill){
                    return bodyColor;
                }else
                {
                    return bodyOffColor;
                }
                default:
                return offColor;
            }
        }else {
            return offColor;
        }
    }

    /// <summary>
    /// The state of skill 
    /// 1 = unlock
    /// 0 = can be unlocked
    /// 1 = still can not be unlocked
    /// </summary>
    public int IsUnlocked(Skill_SO skill){
        int index;
        switch (skill.skillType)
        {
            case SkillType.MELEE:
            index = meleeSkills.IndexOf(skill.id);
            if(index < playerData.meleeSkill.level) return 1;
            if(index == playerData.meleeSkill.level) return 0;
            if(index > playerData.meleeSkill.level) return -1;
            break;
            case SkillType.GUN:
            index = gunSkills.IndexOf(skill.id);
            if(index < playerData.gunSkill.level) return 1;
            if(index == playerData.gunSkill.level) return 0;
            if(index > playerData.gunSkill.level) return -1;
            break;
            case SkillType.BODY:
            index = bodySkills.IndexOf(skill.id);
            if(index < playerData.bodySkill.level) return 1;
            if(index == playerData.bodySkill.level) return 0;
            if(index > playerData.bodySkill.level) return -1;
            break;
        }
        return -1;
    }

    Skill_SO GetSkillSO(string id){
        if(skills.ContainsKey(id)){
            return skills[id];
        }else
        {
            return null;
        }
    }
}
