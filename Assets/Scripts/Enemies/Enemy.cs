﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    private float _health = 100f;
    [SerializeField] private Text _healthText = null;
    public bool IsAlive() { return IsValid() && _health > 0f; }
    public bool IsDead() { return _health == 0f; }
    public bool HasDamage() { return _health < _maxHealth; }
    public bool IsValid() { return this.gameObject.activeInHierarchy; }

    [SerializeField] private float _attackSpeed = 3f;
    private float _attackTimer = 0f;

    [SerializeField] private float _attackDamage = 3f;
    public float GetDamage() { return _attackDamage; }

    private float _zappedTimer = 0f;

    private EnemyMovement _movement = null;

    [SerializeField] private GameObject _deathFX = null;
    [SerializeField] private GameObject _deathScrap = null;

    private void Awake()
    {
        _movement = this.GetComponent<EnemyMovement>();
    }

    private void OnEnable()
    {
        _health = _maxHealth;
        UpdateHealthText();
    }

    //private void Start()
    //{
    //    UpdateHealthText();
    //}

    private void Update()
    {
        if (IsZapped())
        {
            UpdateZappedState();
            return;
        }

        UpdateCombat();
    }

    private void UpdateCombat()
    {
        if (_movement.IsAtTarget())
        {
            _attackTimer += Time.deltaTime;
            if (_attackTimer > _attackSpeed)
            {
                _attackTimer = 0f;

                Attack();
            }
        }
    }

    virtual protected void Attack()
    {
        Truck.Get().TakeDamage(_attackDamage);
    }

    virtual public float TakeDamage(float damage)
    {
        //@TODO: Only damage what is needed and return the extra.
        float adjustedDamage = damage;
        if (damage > _health)
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

    virtual protected void Die()
    {
        PlayDeathSound();

        CreateDeathFX();

        CreateDeathScrap();

        //@TODO: FX and maybe notify AttachmentSystem er Socket?
        this.gameObject.SetActive(false);
    }

    private void PlayDeathSound()
    {
        //Play cat audio
        switch (Random.Range(1, 5))
        {
            case 1:
                AudioController.cat1.Play();
                break;
            case 2:
                AudioController.cat2.Play();
                break;
            case 3:
                AudioController.cat3.Play();
                break;
            case 4:
                AudioController.cat4.Play();
                break;
            default:
                break;
        }
        AudioController.catExplode.Play();
    }

    private void CreateDeathFX()
    {
        Instantiate(_deathFX).transform.position = this.transform.position;
    }

    private void CreateDeathScrap()
    {
        Scrap scrap = Instantiate(_deathScrap).GetComponent<Scrap>();
        scrap.gameObject.SetActive(true);
        scrap.transform.position = this.transform.position;
        scrap.SetEnemyScrap();
    }

    private void UpdateHealthText()
    {
        _healthText.text = _health.ToString("0");

        Color healthColor = HealthColorUtility.GetHealthColor(_health);
        //_healthText.CrossFadeColor(healthColor, 0.5f, false, false);
        _healthText.color = healthColor;
    }

    public void Zap(float damage, float time)
    {
        TakeDamage(damage);
        _zappedTimer = time;
    }

    private void UpdateZappedState()
    {
        if (IsZapped())
        {
            _zappedTimer -= Time.deltaTime;
            if (_zappedTimer <= 0f)
            {
                _zappedTimer = 0f;
            }
        }
    }

    public bool IsZapped()
    {
        return _zappedTimer > 0f;
    }
}
