using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Enemy
{
    [SerializeField] private GameObject _fireNode = null;
    [SerializeField] private GameObject _shellPrefab = null;

    protected override void Attack()
    {
        Fire();
    }

    private void Fire()
    {
        SetupShell();
    }

    private void SetupShell()
    {
        //@TODO: Setup the shell w/ target.
        Shell shell = Instantiate(_shellPrefab).GetComponent<Shell>();
        shell.SetupTarget(_fireNode, Truck.Get().gameObject);
    }
}
