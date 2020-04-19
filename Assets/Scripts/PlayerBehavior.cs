using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    static private PlayerBehavior _instance = null;
    static public PlayerBehavior Get() { return _instance; }

    public ShakeBehavior ShakeBehavior;
    public float MovementSpeed = 1f;

    private Rigidbody2D _rbody;
    private PlayerRenderer _playerRend;
    private float _horizontalInput;
    private float _verticalInput;

    private float _scrapCount = 0;

    private float _health = 100f;

    [SerializeField] private Text _healthText = null;

    [SerializeField] private Text _scrapText = null;

    [SerializeField] private float _activateRadius = 3f;

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogWarning("Should only be one Player instance.");
            Destroy(this.gameObject);
            return;
        }

        _rbody = GetComponent<Rigidbody2D>();
        _playerRend = GetComponentInChildren<PlayerRenderer>();
    }

    private void Start()
    {
        UpdateHealthText();
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

        if(Input.GetKeyDown(KeyCode.E))
        {
            Activate();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            Repair();
        }
    }

    private void Activate()
    {
        //@TODO: Check if we're within range of Truck to do upgrade.
        if(IsInRangeOfTruck())
        {
            DoRandomTruckUpgrade();
        }
    }

    private bool IsInRangeOfTruck()
    {
        //@TODO: Do proper math to check range... probably store radius on player for activation range, etc
        //return true;

        Truck truck = Truck.Get();
        return (truck.transform.position - this.transform.position).sqrMagnitude <= _activateRadius * _activateRadius;
    }

    //@TEMP/@TODO: Later track scrap resources, positioning for which upgrade to do, UI, etc
    private void DoRandomTruckUpgrade()
    {
        Truck truck = Truck.Get();
        truck.DoRangomUpgrade();
    }

    private void Repair()
    {
        if(IsInRangeOfTruck())
        {
            DoRandomTruckRepair();
        }
    }

    //@TEMP/@TODO: Later track scrap resources, positioning for which repair to do, etc
    private void DoRandomTruckRepair()
    {
        const float REPAIR_AMOUNT = 10f;

        Truck truck = Truck.Get();
        truck.DoRandomRepair(REPAIR_AMOUNT);
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

    private void UpdateHealthText()
    {
        _healthText.text = _health.ToString("0");

        Color healthColor = HealthColorUtility.GetHealthColor(_health);
        //_healthText.CrossFadeColor(healthColor, 0.5f, false, false);
        _healthText.color = healthColor;
    }

    public void AddScrap(float amount)
    {
        _scrapCount += amount;

        UpdateScrapText();
    }

    private void UpdateScrapText()
    {
        _scrapText.text = _scrapCount.ToString("0");
    }
}
