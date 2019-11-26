using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Laptop : MonoBehaviour, Interactable
{

    [SerializeField] private GameObject usbFlashDrive;
    [SerializeField] private GameObject wall;
    
    private VideoPlayer screen;
    private GameManager gameManager;
    private AudioSource pluginUsbSound;
    
    void Start(){
        gameManager = Object.FindObjectOfType<GameManager>();
        screen = GetComponent<VideoPlayer>();
        pluginUsbSound = GetComponent<AudioSource>();
    }

    public void OnInteraction(){
        if (usbFlashDrive.active){
            screen.Play();
             StartCoroutine(Code());
        }
    }

    public void OnLookAt(){
        if (gameManager.inventory.Contains("USB")){
            if (usbFlashDrive.active == false){
            usbFlashDrive.SetActive(true);
            pluginUsbSound.Play();
            }
        } 
        else
        {
            gameManager.DisplaySubtitle("the project is on a flashdrive");
        }
    }

    public IEnumerator Code(){
        yield return new WaitForSeconds(31);
        wall.SetActive(false);
    }
}
