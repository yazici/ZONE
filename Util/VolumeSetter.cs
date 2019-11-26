using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSetter : MonoBehaviour
{
    private GameManager gameManager;
    private AudioSource audioSource;
    [SerializeField] private bool music;

    void Start(){
        audioSource = GetComponent<AudioSource>();
        gameManager = Object.FindObjectOfType<GameManager>();
        audioSource.volume = music ? gameManager.musicVolume : gameManager.sfxVolume;
    }
}
