using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UnserializableGameManager : MonoBehaviour
{
    public enum GameState {Preload, Loading, Paused, Play, MainMenu}
    [Header("Loading")]
    [SerializeField] private List<string> sceneNames;
    [SerializeField] int sceneIndex;
    [SerializeField] private bool loadingScreenVanishAfterLoad;
    [SerializeField] private GameObject loadingCanvas;
    [SerializeField] private Slider sunSlider;

    [Header("Game State")]
    public GameState gameState; 
    public bool dev;
    [Header("Gameplay Settings")]
    public Vector2 mouseSensivity;
    public AudioSource musicSource;
    public AudioClip[] musicClips;
    [Range(0, 1)]
    public float sfxVolume;
    [Range(0, 1)]
    public float musicVolume;
   

    void Start()
    {
        Load(sceneNames[sceneIndex]);
    }

    void Update()
    {
        PlayOrPause();
    }

    public void LoadSave()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
        SerializableGameManager serializableGameManager = (SerializableGameManager)bf.Deserialize(file);
        file.Close();
    }

    public void LoadSettings()
    {
        if (!dev)
        {
            musicSource.volume = PlayerPrefs.GetFloat("music");
            sfxVolume = PlayerPrefs.GetFloat("sfx");
            mouseSensivity.x = PlayerPrefs.GetFloat("sensitivity");
            mouseSensivity.y = PlayerPrefs.GetFloat("sensitivity");
        }
        else
            throw new System.Exception("Settings cannot be loaded in dev mode!");
    }
    public void PlayMusic(string clip)
    {
        musicSource.clip = null;
        foreach (AudioClip audioClip in musicClips)
        {
            if (audioClip.name == clip)
            {
                musicSource.clip = audioClip;
                musicSource.Play();
            }
        }
        if (musicSource.clip == null)
            throw new System.Exception("Music clip does not exist in queue!");

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
        sunSlider.value = async.progress;
        while (!async.isDone) {
            yield return null;
        }
    }
    private void Load(string scene)
    {
        loadingCanvas.SetActive(true);
        StartCoroutine(LoadNewScene(scene));
        if (loadingScreenVanishAfterLoad)
            loadingCanvas.SetActive(false);
        else sunSlider.value = 0;
          
    }
}
