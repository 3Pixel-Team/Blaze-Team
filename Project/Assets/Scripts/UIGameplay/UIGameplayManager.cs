using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGameplayManager : MonoBehaviour
{
    public static UIGameplayManager Instance;

    [Header("Player Control")]
    public Joystick joystick;
    public TouchFieldDrag touchField;
    public SkillButton skillMeleeButton;
    public SkillButton skillGunButton;
    public SkillButton skillBodyButton;

    [Header("Health Bar")]
    public Slider healthBar;
    public TextMeshProUGUI healthBarText;

    [Header("Exp Bar")]
    public Slider expBar;
    public TextMeshProUGUI expBarText;

    [Space]
    public TextMeshProUGUI levelText;
    public Button attackButton;
    public TextMeshProUGUI ammoAmountText;

    [Header("Wave")]
    public TextMeshProUGUI waitingWaveStartText;
    public TextMeshProUGUI waveState;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("[UIGameplayManager] There is more then one player Instance");
            return;
        }
        Instance = this;
    }

    void Start(){
        InitSkillControl();
    }

    void InitSkillControl()
    {
        skillMeleeButton.InitSkillButton(SkillManager.Instance.meleeSkill);
        skillGunButton.InitSkillButton(SkillManager.Instance.gunSkill);
        skillBodyButton.InitSkillButton(SkillManager.Instance.bodySkill);
    }

    public void UpdateHealthSlider(PlayerStat playerStats)
    {
        healthBarText.text = playerStats.currentHealth + " / " + playerStats.GetMaxHealth();
        healthBar.maxValue = playerStats.GetMaxHealth();
        healthBar.value = playerStats.currentHealth;
        //add an if for armor / shield
        //healthBar.TakingDamage(amount, playerStats.stats);
    }

    public void UpdateArmorSlider()
    {
    }

    public void UpdateExpSlider(PlayerStat playerStat)
    {
        expBarText.text = playerStat.currentExp + " / " + playerStat.MaxExp();
        expBar.maxValue = playerStat.currentExp;
        expBar.value = playerStat.MaxExp();
    }

    public void UpdateLevelText(PlayerStat playerStat)
    {
        levelText.text = "Level " + playerStat.currentLevel.ToString();
    }

    public void UpdateAmmoText(Item_SO weapon)
    {
        ammoAmountText.text = weapon.currentAmmo + " / " + weapon.magazineSize;
    }
}
