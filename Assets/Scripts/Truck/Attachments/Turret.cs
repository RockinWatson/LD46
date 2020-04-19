using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Attachment
{
    override public void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    private void Update()
    {
        UpdateCombat();
    }

    private void UpdateCombat()
    {
        //@TODO: Build state machine for combat states: targeting, warmup, fire, cooldown, repeat

        UpdateTargeting();

        UpdateFiring();
    }

    private void UpdateTargeting()
    {
        //@TODO: Pick the best / most relevant target

        //@TODO: Turn to face the target?

        //@TODO: Go through warmup
    }

    private void UpdateFiring()
    {

    }
}
