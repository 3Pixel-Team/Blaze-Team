using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillItem : MonoBehaviour
{
    public Image skillBG;
    public Image skillIcon;
    public GameObject toggleActive;
    Skill_SO skill;

    public void InitSkillItem(Skill_SO _skill){
        skill = _skill;
        toggleActive.SetActive(false);
        skillIcon.sprite = skill.skillIcon;
        RefreshSkillItem();
    }

    public void RefreshSkillItem(){
        //toggle state
        if(MainBaseManager.Instance.skill.currentSelectedSkill == skill){
            toggleActive.SetActive(true);
        }else
        {
            toggleActive.SetActive(false);
        }

        skillIcon.color = SkillManager.Instance.GetIconColor(skill);
        skillBG.color = SkillManager.Instance.GetBgColor(skill);
    }

    //button
    public void Click(){
        MainBaseManager.Instance.skill.SelectSkill(skill);
    }
}
