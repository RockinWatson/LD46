using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public ShakeBehavior ShakeBehavior;
    public float MovementSpeed = 1f;

    private Rigidbody2D _rbody;

    private void Awake()
    {
        _rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ShakeBehavior.TriggerShake();
        }
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        var currentPos = _rbody.position;
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        var inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        var movement = inputVector * MovementSpeed;
        var newPos = currentPos + movement * Time.fixedDeltaTime;
        _rbody.MovePosition(newPos);
    }
}
