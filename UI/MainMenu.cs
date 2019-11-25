using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private GameManager gameManager;
    private bool optionsActive = false;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject selections;
    [SerializeField] private AudioSource mouseOverSound;
    [Header("Options")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider sensitivitySlider;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();   
        gameManager.gameState = GameManager.GameState.MainMenu;
        gameManager.PlayMusic(0, true);
        musicSlider.value = PlayerPrefs.GetFloat("music");
        sfxSlider.value = PlayerPrefs.GetFloat("sfx");
        sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity");
    }

  
    public void NewGame()
    {
        gameManager.loadingScreenVanishAfterLoad = true;
        gameManager.LoadNextLevel();
    }

    public void ContinueGame()
    {
        gameManager.loadingScreenVanishAfterLoad = true;    
        gameManager.LoadSave();
       
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenOptions()
    {
        if (optionsActive)
            ApplySettings();
        optionsActive = !optionsActive;
        options.SetActive(optionsActive);
        selections.SetActive(!optionsActive);
       
    }

    public void ApplySettings()
    {
        PlayerPrefs.SetFloat("music", musicSlider.value);
        PlayerPrefs.SetFloat("sfx", sfxSlider.value);
        PlayerPrefs.SetFloat("sensitivity", sensitivitySlider.value);
        gameManager.LoadSettings();
    }
}
