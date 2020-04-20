using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaveTimer : MonoBehaviour
{
    [SerializeField]private float _timer;

    [SerializeField] private GameObject[] _waves;
    [SerializeField] private float[] _waveTimers;

    private Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();

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

    private GameObject[] GetEnemiesOnCurrentWave()
    {
        var enemObjects = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
