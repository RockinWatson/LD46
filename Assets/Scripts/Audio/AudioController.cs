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
    public static AudioSource titleMusic;
    public static AudioSource startGame;
    public static AudioSource storyMusic;
    public static AudioSource shockwave;
    public static AudioSource dogDeath;
    public static AudioSource dogDamage;
    public static AudioSource zapper;
    public static AudioSource starterPistol;

    [SerializeField]
    private bool globalMute;
    private Scene _activeScene;
    private bool _select() { return (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)); }
    private bool _titleIsPlaying;
    private bool _storyIsPlaying;

    // Start is called before the first frame update
    void Awake()
    {
        //globalMute = false;
        _activeScene = SceneManager.GetActiveScene();
        InitAudio();
    }

    // Update is called once per frame
    void Update()
    {
        if ((_activeScene.name == "Title") && (_select()) && (_titleIsPlaying == true))
        {
            StartCoroutine(LoadStory());
        }

        if((IsEndGameScene()) && (_select()) && (_titleIsPlaying == true))
        {
            StartCoroutine(LoadGame());
        }
    }

    private void InitAudio()
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
        titleMusic = audio[13];
        startGame = audio[14];
        storyMusic = audio[15];
        shockwave = audio[16];
        dogDeath = audio[17];
        dogDamage = audio[18];
        zapper = audio[19];
        starterPistol = audio[20];

        for (int i = 0; i < audio.Length; i++)
        {
            audio[i].playOnAwake = false;
            audio[i].loop = false;
        }



        //Check scene
        if (_activeScene.name == "Title")
        {
            _titleIsPlaying = true;
            _storyIsPlaying = false;
            titleMusic.volume = .9f;
            startGame.volume = .3f;
            titleMusic.Play();
            titleMusic.loop = true;
        }
        else if (_activeScene.name == "Story")
        {
            _storyIsPlaying = true;
            _titleIsPlaying = false;
            storyMusic.volume = .9f;
            startGame.volume = .3f;
            storyMusic.Play();
            storyMusic.loop = true;
        }
        else if (_activeScene.name == "GameScene" || _activeScene.name == "AudioTest")
        {

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
            shockwave.volume = .75f;
            dogDeath.volume = .85f;
            dogDamage.volume = .55f;
            zapper.volume = .4f;
            starterPistol.volume = .4f;
            

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
        else if (IsEndGameScene())
        {
            _titleIsPlaying = true;
            _storyIsPlaying = false;
            titleMusic.volume = .9f;
            startGame.volume = .3f;
            titleMusic.Play();
            titleMusic.loop = true;
        }
    }

    IEnumerator LoadStory()
    {
        startGame.Play();
        titleMusic.Stop();
        _titleIsPlaying = false;
        yield return new WaitForSeconds(3.2f);
        SceneManager.LoadScene("Story");
    }

    IEnumerator LoadGame()
    {
        startGame.Play();
        titleMusic.Stop();
        _titleIsPlaying = false;
        yield return new WaitForSeconds(3.2f);
        SceneManager.LoadScene("GameScene");
    }

    private bool IsEndGameScene()
    {
        return (_activeScene.name == "EndGame" || _activeScene.name == "WinGame");
    }
}
