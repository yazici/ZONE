using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
    private Animator animator;

    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }
    
    public void OnLookAt(){
    }

    public void OnInteraction()
    { 
        animator.SetBool("open", !animator.GetBool("open"));
    }

  
}
