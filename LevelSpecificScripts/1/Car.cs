using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Car : Forward
{
    public AudioClip engine, crash;
    public GameManager gameManager;
    public Image blackout;
    public void Start()
    {
        blackout.enabled = false;  
        gameManager = Object.FindObjectOfType<GameManager>();   
        gameManager.PlaySfx(engine, true);       
    }

    public void OnTriggerEnter(Collider collision)
    {
        StartCoroutine(OnCrash());
    }

    IEnumerator OnCrash()
    {
        blackout.enabled = true;
        gameManager.PlaySfx(crash, false);
        yield return new WaitForSeconds(6);
        gameManager.LoadNextLevel();
    }
}
