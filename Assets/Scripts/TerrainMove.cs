using UnityEngine;

public class TerrainMove : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate((Vector2.left + (Vector2.down / 1.4f)) * _speed * Time.fixedDeltaTime);
        if (transform.position.x <= -8.75)
        {
            gameObject.SetActive(false);
        }
    }
}
