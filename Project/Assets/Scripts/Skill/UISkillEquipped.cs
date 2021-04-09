using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISkillEquipped : MonoBehaviour
{
    public Image skillIcon;
    Skill_SO skill;

    public void InitSkillEquipped(Skill_SO _skill){
        skill = _skill;
        if(skill != null){
            skillIcon.gameObject.SetActive(true);
            skillIcon.sprite = skill.skillIcon;
        }else
        {
            skillIcon.gameObject.SetActive(false);
        }
    }

    //click
    public void Click(){
        if(skill != null){
            MainBaseManager.Instance.skill.SelectSkill(skill);
        }
    }
}
