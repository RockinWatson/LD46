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
        for (int i = 0; i < _waves.Length; i++)
        {
            if (_timer >= _waveTimers[i])
            {
                StartWaveText((i + 1).ToString());
                _waves[i].SetActive(true);
                if (i > 0)
                {
                    //De activate the previous Wave if not First wave.
                    _waves[i - 1].SetActive(false);
                }
            }
        }
    }

    private void StartWaveText(string waveInt)
    {
        _text.text = waveInt;
    }
}
