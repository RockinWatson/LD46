using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairFX : MonoBehaviour
{
    private Animator _animator = null;
    private float _timer = 0f;

    private void Awake()
    {
        _animator = this.GetComponent<Animator>();
    }

    private void Start()
    {
        _timer = _animator.GetCurrentAnimatorStateInfo(0).length;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if(_timer <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
