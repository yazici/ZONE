using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour, Interactable
{
    [SerializeField] private AudioClip use;
    [SerializeField] private Light lamp;
    [SerializeField] private Material lampShade;
    [SerializeField] private GameManager gameManager;

    void Start(){
        gameManager = Object.FindObjectOfType<GameManager>();
    }

  
    // Update is called once per frame
    public void OnLookAt(){
       
    }

    public void OnInteraction(){
        lamp.enabled = !lamp.enabled;
        if (lamp.enabled)     
            lampShade.EnableKeyword("_EMISSION");
        else
            lampShade.DisableKeyword("_EMISSION");
        gameManager.PlaySfx(use, false);      
    }
}
