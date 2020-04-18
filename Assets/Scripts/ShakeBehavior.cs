using UnityEngine;

public class ShakeBehavior : MonoBehaviour
{
    // Transform of the GameObject you want to shake
    private new Transform transform;

    // Desired duration of the shake effect
    private float shakeDuration = 0f;

    [SerializeField]
    private float shakeTime = 0.3f;

    // A measure of magnitude for the shake. Tweak based on your preference
    [SerializeField]
    private float shakeMagnitude = 0.2f;

    // A measure of how quickly the shake effect should evaporate
    [SerializeField]
    private float dampingSpeed = 2.0f;

    // The initial position of the GameObject
    Vector3 initialPosition;

    void Awake()
    {
        if (transform == null)
        {
            transform = GetComponent<Transform>();
        }
    }

    void OnEnable()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public void TriggerShake()
    {
        shakeDuration = shakeTime;
    }
}
