using System.Collections;
using System.Threading;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaveTimer : MonoBehaviour
{
    [SerializeField] private float _timer;

    [SerializeField] private GameObject[] _waves;
    [SerializeField] private float[] _waveTimers;

    [SerializeField] private Text _text = null;
    [SerializeField] private Animator _textAnim = null;

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
            StartWaveText("One");
            _waves[0].SetActive(true);
        }
        if (_timer >= _waveTimers[1])
        {
            StartWaveText("Two");
            _waves[0].SetActive(false);
            //TODO: Display Wave Num Text
            _waves[1].SetActive(true);
        }
    }

    private void StartWaveText(string waveInt)
    {
        _text.text = "Wave " + waveInt;  
    }
}
