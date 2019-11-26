using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Wall : MonoBehaviour, Interactable
{
   
   [SerializeField] private GlitchEffect glitchEffect;
   [SerializeField] private SineWave sineWave;
   [SerializeField] private AudioClip lookAtAudio;
   [SerializeField] private VideoPlayer journalVideo;
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

   public void OnInteraction(){

   }

    IEnumerator Effect(){
         glitchEffect.enabled = true;
         sineWave.enabled = true;
         yield return new WaitForSeconds(3);
         journalVideo.Play();
         sineWave.enabled = false;
          glitchEffect.enabled = false;
    }

}
