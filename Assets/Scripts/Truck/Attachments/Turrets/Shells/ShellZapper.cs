using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellZapper : Shell
{
    private Vector3 _direction = Vector3.zero;

    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _range = 3f;
    [SerializeField] private float _damage = 15f;
    [SerializeField] private float _zapTime = 3f;
    [SerializeField] private Color _color = Color.cyan;

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
        if (collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (!enemy.IsZapped())
            {
                enemy.Zap(_damage, _zapTime);

                Enemy newTarget = FindNewZapTarget();
                if (newTarget)
                {
                    SetupTarget(this.gameObject, newTarget.gameObject);
                }
                else
                {
                    Die();
                }
            }
            else
            {
                enemy.TakeDamage(_damage);
            }
        }
    }

    private Enemy FindNewZapTarget()
    {
        Enemy target = null;
        float bestDistance = float.MaxValue;

        Vector3 effectPos = this.transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(effectPos, _range);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if(!enemy.IsZapped())
                {
                    float distSq = (enemy.transform.position - effectPos).sqrMagnitude;
                    if (distSq < bestDistance)
                    {
                        bestDistance = distSq;
                        target = enemy;
                    }
                }
            }
        }

        return target;
    }
}
