using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public Image skillIcon;
    public Image reloadBar;

    Skill_SO skill;

    public void InitSkillButton(Skill_SO _skill)
    {
        skill = _skill;
        if(skill == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            skillIcon.color = SkillManager.Instance.GetIconColor(skill);
            skillIcon.sprite = skill.skillIcon;
            reloadBar.fillAmount = 0;
        }
    }

    //button
    public void ActivateSkill()
    {
        if (reloadBar.fillAmount > 0) return;
        GameObject sObject = Instantiate(skill.skillObject, transform);
        if(sObject.TryGetComponent<Skill>(out Skill skillScript))
        {
            skillScript.ActivateSkill();
            StartCoroutine(Reloading());
        }
    }

    IEnumerator Reloading()
    {
        float duration = skill.coolDownTime;
        reloadBar.fillAmount = 1;
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            reloadBar.fillAmount = duration / (float)skill.coolDownTime;
            yield return null;
        }

    }
}
