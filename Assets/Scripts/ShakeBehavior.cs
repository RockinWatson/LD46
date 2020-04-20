using UnityEngine;

public class ShakeBehavior : MonoBehaviour
{
    static private ShakeBehavior _instance = null;
    static public ShakeBehavior Get() { return _instance; }

    // Transform of the GameObject you want to shake
    private Transform _transform;

    // Desired duration of the shake effect
    private float _shakeDuration = 0f;

    [SerializeField]
    private float _shakeTime = 0.3f;

    // A measure of magnitude for the shake. Tweak based on your preference
    [SerializeField]
    private float _shakeMagnitude = 0.2f;

    // A measure of how quickly the shake effect should evaporate
    [SerializeField]
    private float _dampingSpeed = 2.0f;

    // The initial position of the GameObject
    private Vector3 _initialPosition;

    void Awake()
    {
        if(_instance)
        {
            Debug.LogWarning("Should only be one Screen Shake.");
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        if (_transform == null)
        {
            _transform = GetComponent<Transform>();
        }
    }

    void OnEnable()
    {
        _initialPosition = _transform.localPosition;
    }

    void Update()
    {
        if (_shakeDuration > 0)
        {
            _transform.localPosition = _initialPosition + Random.insideUnitSphere * _shakeMagnitude;

            _shakeDuration -= Time.deltaTime * _dampingSpeed;
        }
        else
        {
            _shakeDuration = 0f;
            _transform.localPosition = _initialPosition;
        }
    }

    public void TriggerShake()
    {
        _shakeDuration = _shakeTime;
    }

    public void TriggerShake(float shakeTime)
    {
        _shakeDuration = shakeTime;
    }
}
