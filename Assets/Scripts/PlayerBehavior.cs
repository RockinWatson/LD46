using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public ShakeBehavior ShakeBehavior;
    public float MovementSpeed = 1f;

    private Rigidbody2D _rbody;
    private PlayerRenderer _playerRend;
    private float _horizontalInput;
    private float _verticalInput;

    private void Awake()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _playerRend = GetComponentInChildren<PlayerRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.B))
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
        var inputVector = new Vector2(_horizontalInput, _verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        var movement = inputVector * MovementSpeed;
        var newPos = currentPos + movement * Time.fixedDeltaTime;
        _playerRend.SetDirection(movement);
        _rbody.MovePosition(newPos);
    }
}
