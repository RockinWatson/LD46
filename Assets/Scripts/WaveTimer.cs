using UnityEngine;

public class WaveTimer : MonoBehaviour
{
    [SerializeField]private float _timer;

    [SerializeField] private GameObject[] _waves;
    [SerializeField] private float[] _waveTimers;

    private void Start()
    {
        foreach (var wave in _waves)
        {
            wave.SetActive(false);
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        WaveActivations();
    }

    private void WaveActivations()
    {
        if (_timer >= _waveTimers[0])
        {
            _waves[0].SetActive(true);
        }
        if (_timer >= _waveTimers[1])
        {
            _waves[0].SetActive(false);
            //TODO: Display Wave Num Text
            _waves[1].SetActive(true);
        }
    }
}
