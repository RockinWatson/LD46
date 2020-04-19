using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Attachment : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    private float _health = 100f;
    [SerializeField] private Text _healthText = null;
    public bool IsAlive() { return _health > 0f; }
    public bool IsDead() { return _health == 0f; }
    public bool HasDamage() { return _health < _maxHealth; }

    private void Start()
    {
        UpdateHealthText();
    }

    virtual public float TakeDamage(float damage)
    {
        //@TODO: Only damage what is needed and return the extra.
        float adjustedDamage = damage;
        if(damage > _health)
        {
            adjustedDamage = _health;
        }

        _health -= adjustedDamage;
        damage -= adjustedDamage;

        UpdateHealthText();

        if (_health <= 0f)
        {
            _health = 0f;

            Die();
        }

        return damage;
    }

    virtual public float RepairDamage(float damage)
    {
        //@TODO: Only heal what is needed and return the extra.
        float adjustedDamage = damage;
        if(_health + damage > _maxHealth)
        {
            adjustedDamage = _maxHealth - _health;
        }

        _health += adjustedDamage;
        damage -= adjustedDamage;

        UpdateHealthText();

        return damage;
    }

    virtual protected void Die()
    {
        //@TODO: FX and maybe notify AttachmentSystem er Socket?
    }

    private void UpdateHealthText()
    {
        _healthText.text = _health.ToString("0");

        Color healthColor = HealthColorUtility.GetHealthColor(_health);
        //_healthText.CrossFadeColor(healthColor, 0.5f, false, false);
        _healthText.color = healthColor;
    }
}
