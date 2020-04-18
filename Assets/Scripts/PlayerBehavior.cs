using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public ShakeBehavior ShakeBehavior;
    public float MovementSpeed = 1f;

    private Rigidbody2D _rbody;
    private bool _facingRight;

    private void Awake()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ShakeBehavior.TriggerShake();
        }
    }

    private void FixedUpdate()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        PlayerMove(horizontalInput, verticalInput);
        Flip(horizontalInput);
    }

    private void PlayerMove(float horizontalInput, float verticalInput)
    {
        var currentPos = _rbody.position;
        var inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        var movement = inputVector * MovementSpeed;
        var newPos = currentPos + movement * Time.fixedDeltaTime;
        _rbody.MovePosition(newPos);
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !_facingRight || horizontal < 0 && _facingRight)
        {
            _facingRight = !_facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
