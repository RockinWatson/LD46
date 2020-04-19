using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    static private Truck _instance = null;
    static public Truck Get() { return _instance; }

    private AttachmentSystem _attachmentSystem = null;

    //@TODO: This is gonna eventually be some separate data structure with all the attachments, their health, etc.
    private float _health = 100f;
    public bool IsAlive() { return _health > 0f; }
    public bool IsDead() { return _health == 0f; }

    private void Awake()
    {
        if(_instance != null)
        {
            Debug.LogWarning("Should only be one Tank instance.");
            return;
        }
        _instance = this;
        _attachmentSystem = this.GetComponentInChildren<AttachmentSystem>();
    }

    public void TakeDamage(float damage)
    {
        float adjustedDamage = _attachmentSystem.TakeDamage(damage);

        _health -= adjustedDamage;

        if(_health <= 0f)
        {
            _health = 0f;

            Die();
        }
    }

    private void Die()
    {
        //@TODO: Fire off some more major death FX / anim and trigger the game ending...
    }

    private void Update()
    {
        //@TODO: Update how the thing will move around within an area of motion in the middle of the playfield to make it feel a bit more lively...

        DEBUG_Input();
    }

    //@TEMP/@NOTE: This is temp until we figure out position indicating what to upgrade and UI, etc
    public void DoRangomUpgrade()
    {
        //@TEMP: Randomly pick armor or turret to upgrade...
        if(Random.Range(0,2) == 0)
        {
            //@TODO: Upgrade random armor piece.
            _attachmentSystem.UpgradeArmor_Random();
        } else
        {
            //@TODO: Upgrade random turret piece.
            _attachmentSystem.UpgradeTurret_Random();
        }
    }

    //@TEMP/@NOTE: This is temp until we figure out position indicating what to repair, etc
    public void DoRandomRepair(float damage)
    {
        //@TODO: Do repair.
        _attachmentSystem.RepairAttachments_Random(damage);
    }

    private void DEBUG_Input()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            DEBUG_RandomDamage();
        }
    }

    private void DEBUG_RandomDamage()
    {
        float damage = Random.Range(5f, 25f);
        _attachmentSystem.TakeDamage(damage);
    }
}
