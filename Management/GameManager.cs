using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Loading")]
    public bool loadingScreenVanishAfterLoad;
    [SerializeField] private List<string> sceneNames;
    [SerializeField] int sceneIndex;
    [SerializeField] private GameObject loadingCanvas;
    [SerializeField] private Slider sunSlider;
    [Header("Game State")]
    public bool dev;
    public GameState gameState; 
    public enum GameState {Preload, Loading, Paused, Play, MainMenu} 
    [Header("Gameplay Settings")]
    [SerializeField] private Text subtitles;
    public List<string> inventory = new List<string>();
    public AudioSource globalSfxSource;
    public AudioSource musicSource;
    public AudioClip[] musicClips;
    [Range(0, 1)]
    public float sfxVolume;
    [Range(0, 1)]
    public float musicVolume;
    [Range(0, 1)]
    public float sensitivity;
   

    void Start()
    {
        Load(sceneNames[sceneIndex]);
        LoadSettings();
    }

    void Update()
    {
        PlayOrPause();
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
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
        if (!dev && PlayerPrefs.HasKey("music"))
        {
            musicSource.volume = PlayerPrefs.GetFloat("music");
            globalSfxSource.volume = PlayerPrefs.GetFloat("sfx");
            sensitivity = PlayerPrefs.GetFloat("sensitivity");      
        }
        else{
            musicSource.volume = musicVolume;
            globalSfxSource.volume = sfxVolume;      
        }
            
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

    public void DisplaySubtitle(string text)
    {   
        StartCoroutine(DisplayText(text));
    }

     IEnumerator DisplayText(string text){
         subtitles.enabled = true;
         subtitles.text = text;
         yield return new WaitForSeconds(5);
         subtitles.enabled = false;
    }
}
