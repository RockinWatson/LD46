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
    public static AudioSource cat1;
    public static AudioSource cat2;
    public static AudioSource cat3;
    public static AudioSource cat4;
    public static AudioSource scrapPickup;
    public static AudioSource laser1;
    public static AudioSource laser2;
    public static AudioSource catExplode;

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
            scrapPickup = audio[9];
            laser1 = audio[10];
            laser2 = audio[11];
            catExplode = audio[12];


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
            cat1.volume = .5f;
            cat2.volume = .5f;
            cat3.volume = .55f;
            cat4.volume = .6f;
            scrapPickup.volume = 8.5f;
            laser1.volume = .4f;
            laser2.volume = .4f;
            catExplode.volume = .25f;

            scrapPickup.reverbZoneMix = 1f;

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
