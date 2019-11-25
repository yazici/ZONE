using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour, Interactable
{
    [SerializeField] private float speed;
    [SerializeField] private AudioClip use;
    private bool closed = true;
    private Vector3 velocity = Vector3.zero;
    private Vector3 doorTarget;
    private GameManager gameManager;

    void Start(){
        gameManager = Object.FindObjectOfType<GameManager>();
    }


    void Update(){
        if (transform.parent.localPosition != doorTarget)
            transform.parent.localPosition = Vector3.MoveTowards(transform.parent.localPosition, doorTarget, speed * Time.deltaTime);
    }
   public void OnLookAt(){
       
    }

    public void OnInteraction(){
      closed = !closed;
      doorTarget = closed ? Vector3.zero :  new Vector3(0,0,0.67f);
      gameManager.PlaySfx(use, false);
    }
}
