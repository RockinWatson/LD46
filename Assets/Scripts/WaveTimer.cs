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

    [SerializeField] private int _finalWave = 4;
    [SerializeField] private float _finalWaveTimeToWin = 2f * 60f; // 2 minutes
    [SerializeField] private string _winSceneName;

    private float _finalWaveTimer = 0f;
    private bool _hasFinalWaveTimerStarted = false;

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

        if(_hasFinalWaveTimerStarted)
        {
            UpdateFinalWaveWinCondition();
        }
    }

    private void WaveActivations()
    {
        for (int i = 0; i < _waves.Length; i++)
        {
            if (_timer >= _waveTimers[i])
            {
                StartWaveText((i + 1));
                _waves[i].SetActive(true);
                if (i > 0)
                {
                    //De activate the previous Wave if not First wave.
                    _waves[i - 1].SetActive(false);
                }
            }
        }
    }

    private void StartWaveText(int waveInt)
    {
        _text.text = waveInt.ToString();

        if(!_hasFinalWaveTimerStarted && waveInt == _finalWave)
        {
            _finalWaveTimer = _finalWaveTimeToWin;
            _hasFinalWaveTimerStarted = true;
        }
    }

    private void UpdateFinalWaveWinCondition()
    {
        _finalWaveTimer -= Time.deltaTime;
        if(_finalWaveTimer <= 0f)
        {
            SceneManager.LoadScene(_winSceneName);
        }
    }
}
