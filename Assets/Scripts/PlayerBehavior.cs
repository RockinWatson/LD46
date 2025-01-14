﻿using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    static private PlayerBehavior _instance = null;
    static public PlayerBehavior Get() { return _instance; }

    public ShakeBehavior ShakeBehavior;
    public float MovementSpeed = 1f;

    private Rigidbody2D _rbody;
    private PlayerRenderer _playerRend;
    private float _horizontalInput;
    private float _verticalInput;

    private float _scrapCount = 0;
    public bool CanAfford(float scrap) { return _scrapCount >= scrap; }
    public void Spend(float scrap) { _scrapCount -= scrap; UpdateScrapText(); }

    private float _health = 100f;

    private bool _playerDead;

    [SerializeField] private Text _healthText = null;

    [SerializeField] private Text _scrapText = null;

    [SerializeField] private float _activateRadius = 3f;

    [SerializeField] private GameObject _repairFX = null;
    [SerializeField] private GameObject _repairNode = null;

    const float REPAIR_COST = 1f;

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogWarning("Should only be one Player instance.");
            Destroy(this.gameObject);
            return;
        }
        _instance = this;

        _rbody = GetComponent<Rigidbody2D>();
        _playerRend = GetComponentInChildren<PlayerRenderer>();
        _playerDead = false;
    }

    private void Start()
    {
        UpdateHealthText();
    }

    // Update is called once per frame
    void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        if(_horizontalInput < 0f)
        {
            _horizontalInput *= 2f;
        }
        _verticalInput = Input.GetAxis("Vertical");
        if (_verticalInput < 0f)
        {
            _verticalInput *= 2f;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            ShakeBehavior.TriggerShake();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            Activate();
        }
        if(Input.GetKeyDown(KeyCode.R) && CanAfford(REPAIR_COST))
        {
            Repair();
        }
    }

    private void Activate()
    {
        //@TODO: Check if we're within range of Truck to do upgrade.
        if(IsInRangeOfTruck())
        {
            DoRandomTruckUpgrade();
        }
    }

    private bool IsInRangeOfTruck()
    {
        //@TODO: Do proper math to check range... probably store radius on player for activation range, etc
        //return true;

        Truck truck = Truck.Get();
        if(IsWithinActivateRange(truck.transform.position)) {
            return true;
        }
        //@TODO: Check trailers...
        List<AttachmentSocket> sockets = truck.GetFunctioningTrailerSockets();
        foreach(var socket in sockets)
        {
            if(IsWithinActivateRange(socket.transform.position))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsWithinActivateRange(Vector3 pos)
    {
        return (pos - this.transform.position).sqrMagnitude <= _activateRadius * _activateRadius;
    }

    //@TEMP/@TODO: Later track scrap resources, positioning for which upgrade to do, UI, etc
    private void DoRandomTruckUpgrade()
    {
        Truck truck = Truck.Get();
        truck.DoRangomUpgrade();
    }

    private void Repair()
    {
        if(IsInRangeOfTruck())
        {
            DoRandomTruckRepair();

            CreateRepairFX();

            Spend(REPAIR_COST);
        }
    }

    //@TEMP/@TODO: Later track scrap resources, positioning for which repair to do, etc
    private void DoRandomTruckRepair()
    {
        const float REPAIR_AMOUNT = 10f;

        Truck truck = Truck.Get();
        truck.DoRandomRepair(REPAIR_AMOUNT);
    }

    private void FixedUpdate()
    { 
        PlayerMove();
    }

    private void PlayerMove()
    {
        var currentPos = _rbody.position;
        var inputVector = new Vector2(_horizontalInput, _verticalInput);
        //inputVector = Vector2.ClampMagnitude(inputVector, 1);
        var movement = inputVector * MovementSpeed;
        var newPos = currentPos + movement * Time.fixedDeltaTime;
        _playerRend.SetDirection(movement);
        _rbody.MovePosition(newPos);
    }

    private void UpdateHealthText()
    {
        _healthText.text = _health.ToString("0");

        Color healthColor = HealthColorUtility.GetHealthColor(_health);
        //_healthText.CrossFadeColor(healthColor, 0.5f, false, false);
        _healthText.color = healthColor;
    }

    public void AddScrap(float amount)
    {
        _scrapCount += amount;

        UpdateScrapText();

        //Play scrap pickup audio
        AudioController.scrapPickup.Play();

        CreateRepairFX();
    }

    private void UpdateScrapText()
    {
        _scrapText.text = _scrapCount.ToString("0");
        GlobalController.Instance.ScrapCollected = Convert.ToInt32(_scrapCount);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            float damage = enemy.GetDamage();
            TakeDamage(damage);
        }
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

        AudioController.dogDamage.Play();

        if (_health <= 0f)
        {
            _health = 0f;

            Die();
        }

        return damage;
    }

    private void Die()
    {
        if (_playerDead == false)
        {
            StartCoroutine(RestartLevel());
        }

        //@TODO: End game, etc.
    }

    IEnumerator RestartLevel()
    {
        AudioController.dogDamage.mute = true;
        AudioController.dogDeath.Play();
        _playerDead = true;
        yield return new WaitForSeconds(3.2f);
        SceneManager.LoadScene("EndGame");
    }

    private void CreateRepairFX()
    {
        GameObject go = Instantiate(_repairFX);
        go.transform.position = _repairNode.transform.position;
    }
}
