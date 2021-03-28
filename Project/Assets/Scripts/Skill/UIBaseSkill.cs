using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIBaseSkill : MonoBehaviour
{
    public Transform meleeParent;
    public Transform gunParent;
    public Transform bodyParent;
    public GameObject skillUIPrefab;
    public UISkillDesc skillDesc;
    public TextMeshProUGUI pointText;
    public UISkillEquipped meleeSkill;
    public UISkillEquipped gunSkill;
    public UISkillEquipped bodySkill;
    public Skill_SO currentSelectedSkill;
    List<UISkillItem> uiSkills = new List<UISkillItem>();
    SkillManager skillManager => SkillManager.Instance;

    public void InitSkillPanel(){
        pointText.text = SaveManager.Instance.playerData.skillPoint.ToString();
        InitSkillList();
        InitSkillEquipped();
        SelectSkill(null);
    }

    public void InitSkillList(){
        for (int i = meleeParent.childCount-1; i > 0; i--)
        {
            DestroyImmediate(meleeParent.GetChild(i).gameObject);
        }
        for (int i = gunParent.childCount-1; i > 0; i--)
        {
            DestroyImmediate(gunParent.GetChild(i).gameObject);
        }
        for (int i = bodyParent.childCount-1; i > 0; i--)
        {
            DestroyImmediate(bodyParent.GetChild(i).gameObject);
        }
        uiSkills = new List<UISkillItem>();
        foreach (var id in skillManager.meleeSkills)
        {
            UISkillItem uiSkill = null;
            uiSkill = Instantiate(skillUIPrefab, meleeParent).GetComponent<UISkillItem>();
            uiSkill.InitSkillItem(skillManager.skills[id]);
            uiSkills.Add(uiSkill);
        }
        foreach (var id in skillManager.gunSkills)
        {
            UISkillItem uiSkill = null;
            uiSkill = Instantiate(skillUIPrefab, gunParent).GetComponent<UISkillItem>();
            uiSkill.InitSkillItem(skillManager.skills[id]);
            uiSkills.Add(uiSkill);
        }
        foreach (var id in skillManager.bodySkills)
        {
            UISkillItem uiSkill = null;
            uiSkill = Instantiate(skillUIPrefab, bodyParent).GetComponent<UISkillItem>();
            uiSkill.InitSkillItem(skillManager.skills[id]);
            uiSkills.Add(uiSkill);
        }
    }

    public void RefreshSkillList(){
        foreach (var uiSkill in uiSkills)
        {
            uiSkill.RefreshSkillItem();
        }
    }

    void InitSkillEquipped(){
        meleeSkill.InitSkillEquipped(skillManager.meleeSkill);
        gunSkill.InitSkillEquipped(skillManager.gunSkill);
        bodySkill.InitSkillEquipped(skillManager.bodySkill);
    }

    public void SelectSkill(Skill_SO skill){
        if(skill == null){
            if(currentSelectedSkill == null){
                currentSelectedSkill = skillManager.skills[skillManager.meleeSkills[0]];
            }
        }else
        {
            currentSelectedSkill = skill;
        }
        RefreshSkillList();
        skillDesc.InitSkillDesc();
    }

    public void RefreshSkillPanel(){
        pointText.text = SaveManager.Instance.playerData.skillPoint.ToString();
        RefreshSkillList();
        InitSkillEquipped();
        skillDesc.InitSkillDesc();
    }
}
