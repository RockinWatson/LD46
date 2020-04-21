using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zapper : Turret
{
    protected override void Fire()
    {
        base.Fire();

        AudioController.zapper.Play();

    }
}
