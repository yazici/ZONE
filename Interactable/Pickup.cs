using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] private string pickupName;
    [SerializeField] private AudioClip pickupSound;
    private GameManager gameManager;

    void Awake(){
        gameManager = Object.FindObjectOfType<GameManager>();
    }

   public void PickupItem(){
        gameManager.inventory.Add(pickupName);
        gameManager.PlaySfx(pickupSound, false);
        OnPickup();
        gameObject.SetActive(false);

   }

   public abstract void OnPickup();
}
