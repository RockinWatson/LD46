using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : Turret
{
    protected override void Fire()
    {
        base.Fire();

        AudioController.shockwave.Play();

    }
}
