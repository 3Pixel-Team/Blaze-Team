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

    public void UpdateHealthSlider(CharacterStats ingamePlayerStats)
    {
        healthBarText.text = ingamePlayerStats.GetHealth() + " / " + ingamePlayerStats.GetMaxHealth();
        healthBar.maxValue = ingamePlayerStats.GetMaxHealth();
        healthBar.value = ingamePlayerStats.GetHealth();
        //add an if for armor / shield
        //healthBar.TakingDamage(amount, playerStats.stats);
    }

    public void UpdateArmorSlider()
    {
    }

    public void UpdateExpSlider(CharacterStats ingamePlayerStats)
    {
        expBarText.text = ingamePlayerStats.GetActualExp() + " / " + ingamePlayerStats.GetMaxExp();
        expBar.maxValue = ingamePlayerStats.GetActualExp();
        expBar.value = ingamePlayerStats.GetMaxExp();
    }

    public void UpdateLevelText(CharacterStats ingamePlayerStats)
    {
        levelText.text = "Level " + ingamePlayerStats.GetLevel().ToString();
    }

    public void UpdateAmmoText(Item_SO weapon)
    {
        ammoAmountText.text = weapon.currentAmmo + " / " + weapon.magazineSize + " (" + weapon.ammoAmountInInv + ") ";
    }
}
