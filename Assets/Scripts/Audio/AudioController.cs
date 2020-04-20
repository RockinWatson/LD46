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
    public AudioSource cat1;
    public AudioSource cat2;
    public AudioSource cat3;
    public AudioSource cat4;

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
            cat1 = audio[5];
            cat2 = audio[6];
            cat3 = audio[7];
            cat4 = audio[8];

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
            cat1.volume = .8f;
            cat2.volume = .8f;
            cat3.volume = .8f;
            cat4.volume = .8f;

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
