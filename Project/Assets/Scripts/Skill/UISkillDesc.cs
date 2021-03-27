using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISkillDesc : MonoBehaviour
{
    public TextMeshProUGUI skillNameText;
    public Image skillBG;
    public Image skillImage;
    public TextMeshProUGUI skillDescText;
    public TextMeshProUGUI skillCoolDownText;
    public TextMeshProUGUI costText;
    public GameObject equipButton;
    public GameObject unequipButton;
    public GameObject unlockButton;
    Skill_SO skill => MainBaseManager.Instance.skill.currentSelectedSkill;

    public void InitSkillDesc(){
        skillNameText.text = skill.skillName;
        skillImage.sprite = skill.skillIcon;
        skillDescText.text = skill.skillDesc;
        skillCoolDownText.text = skill.coolDownTime.ToString();
        costText.text = skill.cost.ToString();
        skillImage.color = SkillManager.Instance.GetIconColor(skill);
        skillBG.color = SkillManager.Instance.GetBgColor(skill);

        equipButton.SetActive(false);
        unequipButton.SetActive(false);
        unlockButton.SetActive(false);
        
        //check if this skill is unlocked
        int lockState = SkillManager.Instance.IsUnlocked(skill);
        if(lockState == 1){
            if(SkillManager.Instance.IsEquipped(skill)){
                unequipButton.SetActive(true);
            }else
            {
                equipButton.SetActive(true);
            }
        }else if(lockState == 0)
        {
            unlockButton.SetActive(true);
        }
    }

    //click 
    public void EquipSkill(){
        SkillManager.Instance.EquipSkill(skill);
        MainBaseManager.Instance.skill.RefreshSkillPanel();
    }

    //click 
    public void UnequipSkill(){
        SkillManager.Instance.UnequipSkill(skill);
        MainBaseManager.Instance.skill.RefreshSkillPanel();
    }

    //click
    public void UnlockSkill(){
        if(SaveManager.Instance.playerData.skillPoint >= skill.cost){
            SaveManager.Instance.playerData.skillPoint -= skill.cost;
            SkillManager.Instance.UnlockSkillData(skill);
            MainBaseManager.Instance.skill.RefreshSkillPanel();
        }
    }
}
