using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, Interactable
{
   
   [SerializeField] private GlitchEffect glitchEffect;
   [SerializeField] private SineWave sineWave;
   [SerializeField] private GameObject lighting;
   [SerializeField] private AudioClip lookAtAudio;
   private GameManager gameManager;
   private bool hasGlitched;

    void Start(){
        gameManager = Object.FindObjectOfType<GameManager>();
    }


    public void OnLookAt(){
        if (!hasGlitched){
            gameManager.PlaySfx(lookAtAudio, false);
            gameManager.PlayMusic(1, false);
            hasGlitched = true;
            StartCoroutine(Effect());
        }
    }

    public void OnInteraction()
    { 
    
    }



    IEnumerator Effect(){
         glitchEffect.enabled = true;
         sineWave.enabled = true;
         yield return new WaitForSeconds(3);
         sineWave.enabled = false;
          glitchEffect.enabled = false;
    }

}
