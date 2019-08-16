using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameState {Preload, Loading, Paused, Play, MainMenu}
    [Header("Loading")]
    [SerializeField] private List<string> sceneNames;
    [SerializeField] int sceneIndex;
    [SerializeField] private GameObject loadingCanvas;

  
    [Header("Game State")]
    public GameState gameState;
    public bool dev;
    [Header("Gameplay Settings")]
    public Vector2 mouseSensivity;
    public AudioSource musicSource;
    public AudioClip[] musicClips;
    [Range(0, 1)]
    public float volume;

    void Start()
    {
        Load(sceneNames[sceneIndex]);
    }

    void Update()
    {
        PlayOrPause();
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
        if (Input.GetKeyDown(KeyCode.Escape))
            if (gameState == GameState.Play && gameState != GameState.MainMenu)
                gameState = GameState.Paused;
            else
                gameState = GameState.Play;
    }

    public void LoadNextLevel()
    {
        sceneIndex++;
        Load(sceneNames[sceneIndex]);
    }

   

    private void Load(string scene)
    {    
        loadingCanvas.SetActive(true);
        if (BoltNetwork.IsRunning)
            BoltNetwork.LoadScene(scene);
        else
            SceneManager.LoadScene(scene);
        loadingCanvas.SetActive(false);


    }
}
