﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
[SerializeField] private float walkSpeed = 6.0f;
    [SerializeField] private float runSpeed = 11.0f;
    [SerializeField] private bool limitDiagonalSpeed = true;
    [SerializeField] private bool toggleRun = false;
    [SerializeField] private float jumpSpeed = 8.0f;
    [SerializeField] private float gravity = 20.0f;
    [SerializeField] private bool slideWhenOverSlopeLimit = false;
    [SerializeField] private bool slideOnTaggedObjects = false;
    [SerializeField] private float slideSpeed = 12.0f;
    [SerializeField] private bool airControl = false;
    [SerializeField] private float antiBumpFactor = .75f;

    private Vector3 moveDirection = Vector3.zero;
    private bool grounded = false;
    private CharacterController controller;
    private float speed;
    private RaycastHit hit;
    private float slideLimit;
    private float rayDistance;
    private Vector3 contactPoint;
    private bool playerControl = false;
    private bool sliding;
    private GameManager gameManager;
    private AudioSource walkingSource;


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        controller = GetComponent<CharacterController>(); 
        speed = walkSpeed;
        rayDistance = controller.height * .5f + controller.radius;
        slideLimit = controller.slopeLimit - .1f;
        walkingSource = GetComponent<AudioSource>();

    }


    private void Update()
    {
        grounded = (controller.Move(moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;
            speed = Input.GetKey(KeyCode.LeftShift) && grounded ? runSpeed : walkSpeed;
        IsSliding();    
        Move();
        Jump();
    }



    public void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        float inputModifyFactor = (inputX != 0.0f && inputY != 0.0f && limitDiagonalSpeed) ? .7071f : 1.0f;
        if (grounded)
        {
            if (sliding && slideWhenOverSlopeLimit || slideOnTaggedObjects && hit.collider.tag == "Slide")
                Slide();
            else{
                PlayerControl(inputX, inputY, inputModifyFactor);
            
            }
        }
        else
            if (airControl && playerControl)
            AirControl(inputX, inputY, inputModifyFactor);
        moveDirection.y -= gravity * Time.deltaTime;
    }

    private void PlayerControl(float inputX, float inputY, float inputModifyFactor)
    {
        moveDirection = new Vector3(inputX * inputModifyFactor, -antiBumpFactor, inputY * inputModifyFactor);
        moveDirection = transform.TransformDirection(moveDirection) * speed;
        playerControl = true;
    }

    private void AirControl(float inputX, float inputY, float inputModifyFactor)
    {
        moveDirection.x = inputX * speed * inputModifyFactor;
        moveDirection.z = inputY * speed * inputModifyFactor;
        moveDirection = transform.TransformDirection(moveDirection);
    }


    private void Jump()
    {
        if (Input.GetButton("Jump") && grounded)
            moveDirection.y = jumpSpeed;
    }

    private void IsSliding()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, rayDistance))
            if (Vector3.Angle(hit.normal, Vector3.up) > slideLimit)
                Slide();
            else
            {
                Physics.Raycast(contactPoint + Vector3.up, -Vector3.up, out hit);
                if (Vector3.Angle(hit.normal, Vector3.up) > slideLimit)
                    Slide();
            }
    }

    private void Slide()
    {
        Vector3 hitNormal = hit.normal;
        moveDirection = new Vector3(hitNormal.x, -hitNormal.y, hitNormal.z);
        Vector3.OrthoNormalize(ref hitNormal, ref moveDirection);
        moveDirection *= slideSpeed;
        playerControl = false;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        contactPoint = hit.point;
    }
    
    /*
  private void Slide(){
        if (!grounded) {
            moveDirection.x += (1f - hitNormal.y) * hitNormal.x * (speed - slideFriction);
            moveDirection.z += (1f - hitNormal.y) * hitNormal.z * (speed - slideFriction);
        }
    }
    */
}
