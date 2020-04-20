using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : Turret
{
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
}
