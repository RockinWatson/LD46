using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class Turret : Attachment
{
    private Enemy _target = null;
    private float _attackTimer = 0f;

    [SerializeField] private GameObject _fireNode = null;
    [SerializeField] private GameObject _shellPrefab = null;
    [SerializeField] private float _attackSpeed = 3f;
    [SerializeField] private float _attackRange = 8f;

    override public float TakeDamage(float damage)
    {
        return base.TakeDamage(damage);
    }

    private void Update()
    {
        UpdateCombat();
    }

    private void UpdateCombat()
    {
        //@TODO: Build state machine for combat states: targeting, warmup, fire, cooldown, repeat

        UpdateTargeting();

        if (IsTargetValid(_target))
        {
            UpdateFiring();
        }
    }

    private void UpdateTargeting()
    {
        if (!IsTargetValid(_target))
        {
            _target = null;

            //@TODO: Pick the best / most relevant target
            var enemies = GetEnemyTargets();
            Enemy bestTarget = null;
            float bestDistance = float.MaxValue;
            foreach(Enemy enemy in enemies)
            {
                if(IsTargetValid(enemy))
                {
                    float distSq = (enemy.transform.position - this.transform.position).sqrMagnitude;
                    if (distSq < bestDistance)
                    {
                        bestTarget = enemy;
                        bestDistance = distSq;
                    }
                }
            }

            _target = bestTarget;
            _attackTimer = 0f;
        }

        //@TODO: Turn to face the target?
        //if(IsTargetValid(_target))
        //{
        //}
    }

    private void UpdateFiring()
    {
        //@TODO: Go through warmup
        _attackTimer += Time.deltaTime;
        if(_attackTimer >= _attackSpeed)
        {
            _attackTimer = 0f;

            Fire();
        }
    }

    //@TODO: Check that we have a valid target (non-null and not dead)
    private bool IsTargetValid(Enemy target)
    {
        if(target && target.IsAlive())
        {
            if((target.transform.position - this.transform.position).sqrMagnitude <= _attackRange* _attackRange)
            {
                return true;
            }
        }
        return false;
    }

    //@TODO: Maybe use new Enemy type instead of EnemyMovement
    //@TODO: Maybe pull from some global managed list of Enemies to avoid allocations...
    private List<Enemy> GetEnemyTargets()
    {
        return FindObjectsOfType<Enemy>().ToList();
    }

    virtual protected void Fire()
    {
        SetupShell();
    }

    private void SetupShell()
    {
        //@TODO: Setup the shell w/ target.
        Shell shell = Instantiate(_shellPrefab).GetComponent<Shell>();
        shell.SetupTarget(_fireNode, _target.gameObject);
    }
}
