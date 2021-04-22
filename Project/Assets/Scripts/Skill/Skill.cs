using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is parent class of all skills
public class Skill : MonoBehaviour
{
    public Skill_SO skillSO;

    public virtual void ActivateSkill()
    {
       
    }

    public virtual void DeactivateSkill()
    {
        Destroy(gameObject);
    }
}
