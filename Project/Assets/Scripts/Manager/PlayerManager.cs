using System;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("[PlayerManager] There is more then one player Instance");
            return;
        }
        Instance = this;
    }


    public event EventHandler OnLevelChanged;

    public event EventHandler OnExpChanged;



    public PlayerStat playerStat;

    public PlayerAnimator playerAnimator;

    public AttackDefenition baseAttack;

    public ProjectileManager projectileManager;

    public Item_SO weapon;

    //shooting variables
    private bool readyToShoot = true;

    private bool reloading = false;
    public bool isWeaponRaycast;
    public int currentAmmo;

    public MissionInventory missionInventory;

    UIGameplayManager uiGameplay => UIGameplayManager.Instance;

    #region Start and Update

    private void Start()
    {

        playerStat = GetComponent<PlayerStat>();
        projectileManager = GetComponent<ProjectileManager>();

        //Initialize the two events with their method
        OnLevelChanged += OnLevelChange;
        OnExpChanged += OnExpChange;

        //updates all the UI
        UpdateAmmoText();
        RefreshStats();
        UpdateLevelText();
        //missionInventory.ResetBag();
        //SkillTreeManager.Instance.UpdateAvailablePoints();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10);
            UpdateHealthSlider();
        }

        if (Input.GetKeyDown(KeyCode.L)) //Debug purposes, can be removed at any time
        {
            GiveExp(40);
            Debug.Log($"Actual level:{playerStat.currentLevel} \n Actual Exp:{playerStat.currentExp}   Actual Max EXP:{playerStat.MaxExp()}");
            //Remember to delete the Debug.log in the OnLevelChange() method when you are not longer going to use this debug tool.
        }

        //I dont have Smaprthone so I am shooting with mouse click
        if (Input.GetMouseButtonDown(0))
            projectileManager.ShootWeapon(isWeaponRaycast);
    }

    #endregion Start and Update

    #region Animations

    public void ShootingAnimation()
    {
        playerAnimator.Shooting();
    }

    #endregion Animations

    #region Pickup via Collision
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ItemPickup>())
        {
            var item = other.GetComponent<ItemPickup>();
            if (item)
            {
                //missionInventory.AddItem(item.item, 1);
                Destroy(other.gameObject);
            }
        }
    }

    #endregion Pickup via Collision

    #region UI Updates

    public void UpdateHealthSlider()
    {
        uiGameplay?.UpdateHealthSlider(playerStat);
    }

    public void UpdateArmorSlider()
    {
    }

    public void UpdateExpSlider()
    {
        uiGameplay?.UpdateExpSlider(playerStat);
    }

    public void UpdateLevelText()
    {
        uiGameplay?.UpdateLevelText(playerStat);
    }

    public void UpdateAmmoText()
    {
        uiGameplay?.UpdateAmmoText(weapon);
    }

    public void UpdateStatusBarText(TextMeshProUGUI barText, string min, string max)
    {
        barText.text = min + " / " + max;
    }

    #endregion UI Updates

    #region Stats Updates

    public void RefreshStats()
    {
        playerStat.InitCharacterStat();

        UpdateArmorSlider();
        UpdateExpSlider();
        UpdateHealthSlider();
    }

    #endregion 

    #region Increasers

    public void GiveHealth(int amount)
    {
        playerStat.GiveHealth(amount);
    }

    public void GiveShield(int amount)
    {
        playerStat.GiveShield(amount);
    }

    public void GiveCredit(int amount)
    {
        playerStat.GiveCredit(amount);
    }

    public void GiveExp(int amount)
    {
        playerStat.GiveExp(amount);
    }

    #endregion 

    #region Decreasers

    public void TakeDamage(int amount)
    {
        playerStat.TakeDamage(amount);
        if (playerStat.currentHealth <= 0)
        {
            Debug.Log("Player Died");

            GameManager.Instance.DeathEventCall();
        }

        UpdateHealthSlider();
    }

    #endregion Decreasers

    #region Attacking

    public void OnProjectileCollided(GameObject target)
    {
        var attack = baseAttack.CreateAttack(playerStat, target.GetComponent<CharacterStats>());

        var attackable = target.GetComponentsInChildren<IAttackable>();

        foreach (IAttackable e in attackable)
        {
            e.OnAttack(gameObject, attack);
        }
    }

    public void Shooting()
    {
        if (readyToShoot == false || reloading == true)
        {
            return;
        }

        readyToShoot = false;
        if (weapon.currentAmmo > 0)
        {
            weapon.currentAmmo--;

            projectileManager.ShootWeapon(isWeaponRaycast);
            AudioManager.Instance.Play("Shoot");

            UpdateAmmoText();
            Invoke("ResetShot", weapon.shotsPerSec);
        }
        else
        {
            Debug.Log("[Player Manager] You dont have enought ammo!");
            ResetShot();
            return;
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        AudioManager.Instance.Play("Reload");
        Invoke("ReloadFinished", weapon.reloadTime);
    }

    private void ReloadFinished()
    {
        //reset magazine and remove the ammo from the inventory
        weapon.currentAmmo = weapon.magazineSize;
        UpdateAmmoText();
        reloading = false;
        ResetShot();
    }

    #endregion Attacking

    #region Event Calls

    public void LevelUpEventCall()
    {
        OnLevelChanged?.Invoke(this, EventArgs.Empty);
    }

    public void ExpChangeEventCall()
    {
        OnExpChanged?.Invoke(this, EventArgs.Empty);
    }

    #endregion Event Calls

    #region Leveling Up and EXP

    private void OnLevelChange(object sender, EventArgs e) //Method called onlevelchanged event
    {
        //Visual effects or things that happen on the event of leveling up
        playerStat.LevelUpStatsChange();
        UpdateExpSlider();
        UpdateLevelText();
        RefreshStats();
        //SkillTreeManager.Instance.AddSkillPoints(3);
        Debug.Log($"LEVEL UP! \n New Stats:  MAXHEALTH = {playerStat.currentHealth} MAXSHIELD = {playerStat.currentShield} BASEARMOR = {playerStat.currentDefense}  "); //Debug purposes, can be removed at any time
    }

    private void OnExpChange(object sender, EventArgs e) //Method called onexpchanged event
    {
        //Visual effects or things that happen on the event of getting exp
        UpdateExpSlider();
    }

    #endregion Leveling Up and EXP

    public void AddAmmo(int size)
    {
        if (currentAmmo >= weapon.magazineSize)
        {
            Debug.Log("Ammo is full");
            return;
        }

        currentAmmo += size;
        if(currentAmmo > weapon.magazineSize)
        {
            currentAmmo = weapon.magazineSize;
        }
    }
        
}