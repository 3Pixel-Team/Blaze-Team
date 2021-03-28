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
    SkillManager skillManager => SkillManager.Instance;

    public void InitSkillDesc(){
        skillNameText.text = skill.skillName;
        skillImage.sprite = skill.skillIcon;
        skillDescText.text = skill.skillDesc;
        skillCoolDownText.text = skill.coolDownTime.ToString();
        costText.text = skill.cost.ToString();
        skillImage.color = skillManager.GetIconColor(skill);
        skillBG.color = skillManager.GetBgColor(skill);

        equipButton.SetActive(false);
        unequipButton.SetActive(false);
        unlockButton.SetActive(false);
        
        //check if this skill is unlocked
        int lockState = skillManager.IsUnlocked(skill);
        if(lockState == 1){
            if(skillManager.IsEquipped(skill)){
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
        skillManager.EquipSkill(skill);
        MainBaseManager.Instance.skill.RefreshSkillPanel();
    }

    //click 
    public void UnequipSkill(){
        skillManager.UnequipSkill(skill);
        MainBaseManager.Instance.skill.RefreshSkillPanel();
    }

    //click
    public void UnlockSkill(){
        if(SaveManager.Instance.playerData.skillPoint >= skill.cost){
            SaveManager.Instance.playerData.skillPoint -= skill.cost;
            skillManager.UnlockSkillData(skill);
            MainBaseManager.Instance.skill.RefreshSkillPanel();
        }
    }
}
