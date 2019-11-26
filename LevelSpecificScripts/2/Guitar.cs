using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guitar : MonoBehaviour, Interactable
{
    private AudioSource guitarSound;
    // Start is called before the first frame update
    void Start()
    {
        guitarSound = GetComponent<AudioSource>();
    }

    public void OnLookAt(){
       
    }

    public void OnInteraction(){
        if (!guitarSound.isPlaying)
            guitarSound.Play();
    }
}
