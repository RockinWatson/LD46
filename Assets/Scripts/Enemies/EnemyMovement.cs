using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _rangeToStop;

    private bool _isAtTarget = false;
    private bool _isCollidingWithEnemy = false;

    private void OnEnable()
    {
        _isAtTarget = false;
    }

    private void Update()
    {
        IsAtTarget();
    }

    private void IsAtTarget()
    {
        var distanceToTarget = Vector3.Distance(_target.transform.position, transform.position);
        if (distanceToTarget <= _rangeToStop ||
           (distanceToTarget <= _rangeToStop && _isCollidingWithEnemy))
        {
            _isAtTarget = true;
        }
    }

    private void FixedUpdate()
    {
        GoToTarget();      
    }

    private void GoToTarget()
    {
        if (_isAtTarget == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.fixedDeltaTime);
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "BadCat1")
        {
            _isCollidingWithEnemy = true;
        }
    }
}
