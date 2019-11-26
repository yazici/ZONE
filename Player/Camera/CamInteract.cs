using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamInteract : MonoBehaviour
{
    public float distance;
    private RaycastHit hit;

    void Update()
    {     
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance)) 
        {
            if(hit.transform.tag == "Interactable")          
                Interact();    
            else if (hit.transform.tag == "Pickup")
                Pickup();
        }
    } 

    void Interact(){
        Interactable interactable = hit.collider.GetComponent<Interactable>();
        interactable.OnLookAt();
        if (Input.GetKeyDown(KeyCode.E))                    
            interactable.OnInteraction();
    }

    void Pickup(){
        Pickup pickup = hit.collider.GetComponent<Pickup>();
        if (Input.GetKeyDown(KeyCode.E))           
            pickup.PickupItem();    
    }
    
}