﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellLazer : Shell
{
    private Vector3 _direction = Vector3.zero;

    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _damage = 15f;

    private void Start()
    {
        SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
        sprite.color = Color.magenta;
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
        if (collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            //@TODO: Do damage from shell.
            enemy.TakeDamage(_damage);

            Die();
        }
    }
}