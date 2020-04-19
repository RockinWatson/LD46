using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachment : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth = 100f;
    private float _health = 100f;

    public bool HasDamage() { return _health < _maxHealth; }

    virtual public void TakeDamage(float damage)
    {
        //@TODO: Only damage what is needed and return the extra.

        _health -= damage;

        if(_health <= 0f)
        {
            _health = 0f;

            Die();
        }
    }

    virtual public void RepairDamage(float damage)
    {
        //@TODO: Only heal what is needed and return the extra.

        _health += damage;
    }

    virtual protected void Die()
    {
        //@TODO: FX and maybe notify AttachmentSystem er Socket?
    }
}
