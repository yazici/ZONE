using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<string> inventory = new List<string>();
    public enum GameState {Preload, Loading, Paused, Play, MainMenu}
    [Header("Loading")]
    public bool loadingScreenVanishAfterLoad;
    [SerializeField] private List<string> sceneNames;
    [SerializeField] int sceneIndex;
    [SerializeField] private GameObject loadingCanvas;
    [SerializeField] private Slider sunSlider;

    [Header("Game State")]
    public GameState gameState; 
    public bool dev;
    [Header("Gameplay Settings")]
    public AudioSource globalSfxSource;
    public AudioSource musicSource;
    public AudioClip[] musicClips;
    public Vector2 mouseSensivity;
    [Range(0, 1)]
    public float sfxVolume;
    [Range(0, 1)]
    public float musicVolume;
   

    void Start()
    {
        Load(sceneNames[sceneIndex]);
        LoadSettings();
    }

    void Update()
    {
        PlayOrPause();
    }

    public void LoadSave()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
        SaveData serializableGameManager = (SaveData)bf.Deserialize(file);
        file.Close();
    }

    public void LoadSettings()
    {
        if (!dev)
        {
            musicSource.volume = PlayerPrefs.GetFloat("music");
            globalSfxSource.volume = PlayerPrefs.GetFloat("sfx");
            mouseSensivity.x = PlayerPrefs.GetFloat("sensitivity");
            mouseSensivity.y = PlayerPrefs.GetFloat("sensitivity");
        }
        else
            throw new System.Exception("Settings cannot be loaded in dev mode!");
    }


    public void PlayMusic(int clip, bool loop)
    {
        musicSource.Stop();
        musicSource.loop = loop;
        musicSource.clip = musicClips[clip];
        musicSource.Play();
    }

    public void PlaySfx(AudioClip clip, bool loop)
    {

        globalSfxSource.Stop();
        globalSfxSource.loop = loop;
        globalSfxSource.clip = clip;
        globalSfxSource.Play();
    }


    private void PlayOrPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameState != GameState.MainMenu)
            if (gameState == GameState.Play)
                gameState = GameState.Paused;
            else
                gameState = GameState.Play;
    }

    public void LoadNextLevel()
    {
        sceneIndex++;
        Load(sceneNames[sceneIndex]);
    }


    IEnumerator LoadNewScene(string scene)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
        loadingCanvas.SetActive(true);
        musicSource.Stop();
        while (!async.isDone)
        {
            sunSlider.value = async.progress;

                yield return null;

        }
        loadingCanvas.SetActive(!loadingScreenVanishAfterLoad);
        sunSlider.value = 0;

    }
    private void Load(string scene)
    {   
        StartCoroutine(LoadNewScene(scene));
    }
}
