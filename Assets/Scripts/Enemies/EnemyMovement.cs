using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _rangeToStop;

    private bool _isAtTarget = false;
    public bool IsAtTarget() { return _isAtTarget; }
    private bool _isCollidingWithEnemy = false;

    private void OnEnable()
    {
        _isAtTarget = false;
    }

    private void Update()
    {
        CheckIsAtTarget();
    }

    private void CheckIsAtTarget()
    {
        float distanceToTargetSq = (_target.transform.position - transform.position).sqrMagnitude;
        if (distanceToTargetSq <= _rangeToStop*_rangeToStop ||
           (distanceToTargetSq <= _rangeToStop * _rangeToStop && _isCollidingWithEnemy))
        {
            _isAtTarget = true;
        }
        else
        {
            _isAtTarget = false;
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
        if (collision.gameObject.name == "Truck")
        {
            _isCollidingWithEnemy = true;
        }
    }
}
