using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    private GameObject _target = null;
    private Vector3 _direction = Vector3.zero;

    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _damage = 15f;
    public float GetDamage() { return _damage; }

    private void Start()
    {
        SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
        sprite.color = Color.magenta;
    }

    public void SetupTarget(GameObject source, GameObject target)
    {
        this.transform.position = source.transform.position;

        _target = target;

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

    public void Die()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            //@TODO: Do damage from shell.
            float damage = GetDamage();
            enemy.TakeDamage(damage);

            Die();
        }
    }
}
