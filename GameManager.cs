using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public enum GameState {Paused, Play, MainMenu}
    [Header("Loading")]
    [SerializeField] private List<string> sceneNames;
    [SerializeField] int sceneIndex;
    [SerializeField] private Canvas loadingCanvas;
    [SerializeField] private Slider loadingBarImg;
    [Header("Player")]
    public Vector2 mouseSensivity;
    [Header("Game State")]
    public GameState gameState;
    public bool dev;


    void Start()
    {
        loadingCanvas.gameObject.SetActive(false);
        Load(sceneNames[sceneIndex]);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (gameState == GameState.Play && gameState != GameState.MainMenu)
                Pause();
            else
                Resume();
    }

    private void Pause()
    {
        gameState = GameState.Paused;
        determiniePauseOrResume();
    }

    private void Resume()
    {
       
        gameState = GameState.Play;
        determiniePauseOrResume();

    }

    private void determiniePauseOrResume()
    {
      
        bool enabled = gameState == GameState.Play ? true : false;
        //GameObject.Find("Player").GetComponentInChildren<MouseLook>().enabled = enabled;
        Time.timeScale = enabled ? 1 : 0;
        
    }


    public void LoadNextLevel()
    {
        sceneIndex++;
        Load(sceneNames[sceneIndex]);
    }

    private void Load(string scene)
    {
        StartCoroutine(LoadScene(scene));
    }

    private IEnumerator LoadScene(string scene)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
        loadingCanvas.gameObject.SetActive(true);
        while (!async.isDone)
        {
            loadingBarImg.value = async.progress;
            yield return null;
        }
        loadingCanvas.gameObject.SetActive(false);
    }


}
