using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private UnserializableGameManager gameManager;
    private bool optionsActive = false;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject selections;
    [Header("Options")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider sensitivitySlider;

    private void Start()
    {
        gameManager = FindObjectOfType<UnserializableGameManager>();
        musicSlider.value = PlayerPrefs.GetFloat("music");
        sfxSlider.value = PlayerPrefs.GetFloat("sfx");
        sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity");

    }


    public void NewGame()
    {
        gameManager.LoadNextLevel();
    }

    public void ContinueGame()
    {
        gameManager.LoadSave();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenOptions()
    {

        optionsActive = !optionsActive;
        options.SetActive(optionsActive);
        selections.SetActive(!optionsActive);
        if (optionsActive)
            ApplySettings();
    }

    public void ApplySettings()
    {
        PlayerPrefs.SetFloat("music", musicSlider.value);
        PlayerPrefs.SetFloat("sfx", sfxSlider.value);
        PlayerPrefs.SetFloat("sensitivity", sensitivitySlider.value);
        gameManager.LoadSettings();
    }
}
