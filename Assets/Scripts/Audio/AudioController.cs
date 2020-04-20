using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public static AudioSource levelMusic;
    public static AudioSource build;
    public static AudioSource repair;
    public static AudioSource truckDamage;
    public static AudioSource truckExplode;

    [SerializeField]
    private bool globalMute;
    private Scene _activeScene;

    // Start is called before the first frame update
    void Awake()
    {
        globalMute = false;
        _activeScene = SceneManager.GetActiveScene();
        InitAudio();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitAudio()
    {
        //Check scene
        if (_activeScene.name == "GameScene" || _activeScene.name == "AudioTest");
        {
            //Initialize audio components
            AudioSource[] audio = GetComponents<AudioSource>();
            levelMusic = audio[0];
            build = audio[1];
            repair = audio[2];
            truckDamage = audio[3];
            truckExplode = audio[4];

            for (int i = 0; i < audio.Length; i++)
            {
                audio[i].playOnAwake = false;
                audio[i].loop = false;
            }

            //Set initial volumes
            levelMusic.volume = .85f;
            build.volume = 1f;
            repair.volume = .4f;
            truckDamage.volume = .6f;
            truckExplode.volume = .8f;

            //Play and loop 
            levelMusic.Play();
            levelMusic.loop = true;

            if (globalMute == true)
            {
                for (int i = 0; i < audio.Length; i++)
                {
                    audio[i].volume = 0f;
                }
            }

        }
    }
}
