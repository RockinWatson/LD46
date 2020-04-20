using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellShockwave : Shell
{
    [SerializeField] private float _range = 5f;
    [SerializeField] private float _countdown = 1f;
    [SerializeField] private float _force = 20f;
    [SerializeField] private float _damage = 15f;

    private float _timer = 0f;

    override public void SetupTarget(GameObject source, GameObject target)
    {
        base.SetupTarget(source, target);

        //@TODO: Set off screen shake?
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= _countdown)
        {
            GoOff();
        }
    }

    private void GoOff()
    {
        //@TODO: Find all Enemies within range of initial target and fuck them up.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_target.transform.position, _range);
        foreach(Collider2D collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                enemy.TakeDamage(_damage);

                Vector3 effectPos = this.transform.position;
                Rigidbody2D rigidBody = collider.GetComponent<Rigidbody2D>();
                Vector2 force = (enemy.transform.position - effectPos).normalized * _force;
                rigidBody.AddForceAtPosition(force, effectPos, ForceMode2D.Impulse);
            }
        }

        Die();
    }
}
