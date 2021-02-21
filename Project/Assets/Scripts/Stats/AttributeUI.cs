using TMPro;
using UnityEngine;

public class AttributeUI : MonoBehaviour
{

    public AttributeUI_SO attribute;

    private CharacterStats _stats;

    public GameObject attributeAddButton;

    public TextMeshProUGUI title;
    public TextMeshProUGUI amountOfAttribute;
    void Start()
    {
        _stats = PlayerManager.Instance.ingamePlayerStats;
        title.text = attribute.attributeName;
    }
    public void UpdateText()
    {
        if (_stats == null)
        {
            _stats = PlayerManager.Instance.ingamePlayerStats;
        }
        switch (attribute.attributeType)
        {
            case TypeOfAttributes.HP:
                amountOfAttribute.text = _stats.GetMaxHealth().ToString();
                break;

            case TypeOfAttributes.SHIELD:
                amountOfAttribute.text = _stats.GetMaxShield().ToString();
                break;

            case TypeOfAttributes.DAMAGE:
                amountOfAttribute.text = _stats.GetDamage().ToString();
                break;

            case TypeOfAttributes.DEFENCE:
                amountOfAttribute.text = _stats.GetArmor().ToString();
                break;

            case TypeOfAttributes.CRITCHANCE:
                amountOfAttribute.text = _stats.GetCriticalChance().ToString("#.00") + " %"; ;
                break;

            case TypeOfAttributes.EMPTY:
                break;
        }

    }

    public void IsActive(bool active)
    {
        attributeAddButton.SetActive(active);
    }

    public void AddAttribute()
    {
        if (_stats == null)
        {
            _stats = PlayerManager.Instance.ingamePlayerStats;
        }
        switch (attribute.attributeType)
        {
            case TypeOfAttributes.HP:
                _stats.stats.maxHealth += (int)attribute.addValue;
                break;

            case TypeOfAttributes.SHIELD:
                _stats.stats.maxShield += (int)attribute.addValue;
                break;

            case TypeOfAttributes.DAMAGE:
                _stats.stats.baseDamage += (int)attribute.addValue;
                break;

            case TypeOfAttributes.DEFENCE:
                _stats.stats.baseArmor += (int)attribute.addValue;
                break;

            case TypeOfAttributes.CRITCHANCE:
                _stats.stats.criticalChance += attribute.addValue;
                break;

            case TypeOfAttributes.EMPTY:
                break;
        }
    }

    public void OnClickAddAttribute()
    {
        if (_stats == null)
        {
            _stats = PlayerManager.Instance.ingamePlayerStats;
        }
        SkillTreeManager.Instance.AddAttribute(this);
    }
}
