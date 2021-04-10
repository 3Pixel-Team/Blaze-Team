using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRegen : Skill
{
    public int regenAmount;

    public override void ActivateSkill()
    {
        base.ActivateSkill();

        PlayerManager.Instance.GiveHealth(regenAmount);

        DeactivateSkill();
    }
}
