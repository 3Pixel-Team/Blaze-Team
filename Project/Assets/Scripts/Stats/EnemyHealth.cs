using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    public EnemyInfoSO enemyInfo;

    private Slider enemyHealthBar;
    public float currentHealth;
    public float maxHealth;
    

    private void Start()
    {
        maxHealth = 1;
        enemyHealthBar.maxValue = maxHealth;
    }

    void Awake()
    {
        enemyHealthBar = GetComponent<Slider>();
    }

   


    void Update()
    {
        if (currentHealth < maxHealth)
        {

            enemyHealthBar.value = currentHealth;
        } else
        {
            enemyHealthBar.value = 0;
        }
    }
}
