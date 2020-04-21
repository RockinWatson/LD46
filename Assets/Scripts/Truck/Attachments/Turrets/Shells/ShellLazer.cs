using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellLazer : Shell
{
    private Vector3 _direction = Vector3.zero;

    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _damage = 15f;
    [SerializeField] private Color _color = Color.magenta;
    [SerializeField] private bool _hurtEnemy = true;
    [SerializeField] private bool _hurtTruck = false;
    [SerializeField] private bool _hurtPlayer = false;

    private void Start()
    {
        SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
        sprite.color = _color;
    }

    override public void SetupTarget(GameObject source, GameObject target)
    {
        base.SetupTarget(source, target);

        _direction = (_target.transform.position - this.transform.position).normalized;
    }

    private void FixedUpdate()
    {
        transform.Translate(_direction * Time.fixedDeltaTime * _speed);
    }

    private void OnBecameInvisible()
    {
        Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_hurtEnemy && collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamage(_damage);

            Die();
        } else if (_hurtTruck && collision.tag == "Truck")
        {
            Truck.Get().TakeDamage(_damage);

            Die();
        }
        else if (_hurtPlayer && collision.tag == "Player")
        {
            PlayerBehavior player = collision.GetComponent<PlayerBehavior>();
            player.TakeDamage(_damage);

            Die();
        }
    }
}
