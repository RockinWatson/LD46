using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerFreebie : Turret
{
    protected override void Awake()
    {
        //@NOTE: Just hiding base behavior.
    }

    protected override void Start()
    {
        //@NOTE: Just hiding base behavior.
    }

    public override float TakeDamage(float damage)
    {
        //@NOTE: Takes no damage.
        return damage;
    }

    protected override void Update()
    {
        UpdateCombat();
    }

    protected override void Fire()
    {
        base.Fire();

        switch (Random.Range(1, 3))
        {
            case 1:
                AudioController.laser1.Play();
                break;
            case 2:
                AudioController.laser2.Play();
                break;
            default:
                break;
        }
    }

    protected override void Die()
    {
        //@TODO: We never die. Goonies live forever.
    }
}
