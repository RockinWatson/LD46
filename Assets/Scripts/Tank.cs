using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    //@TODO: This is gonna eventually be some separate data structure with all the attachments, their health, etc.
    private float _health = 100f;
    public bool IsAlive() { return _health > 0f; }
    public bool IsDead() { return _health == 0f; }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if(_health <= 0f)
        {
            _health = 0f;

            Die();
        }
    }

    private void Die()
    {
        //@TODO: Fire off some more major death FX / anim and trigger the game ending...
    }
}
