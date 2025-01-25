using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC : MonoBehaviour
{
    [Header("References")]
    protected CharacterController controller;
    

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float gravity = 0f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private AudioSource Marche;
    [SerializeField] private AudioSource Jump;
    [SerializeField] private AudioSource Create_Bulle;

    [Header("References Settings")]
    [SerializeField] protected GameObject BulleMesh;

    private float verticalVelocity;

    [Header("Input")]
    private float moveInput;
    private float turnInput;


    private Boolean bDejaTirerBulle;
    private int nbreBulle;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        InputManagement();
        Movement();
    }

    private void Movement()
    {
        GroundMovement();
        Interaction();
        Bulle();
    }

    private void GroundMovement()
    {
        Vector3 move = new Vector3(turnInput, 0, moveInput);
        
        move.y = VerticalForceCalculation();

        move *= walkSpeed;

        controller.Move(move * Time.deltaTime);
    }

    private float VerticalForceCalculation()
    {
        if (controller.isGrounded)
        {

            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * gravity * 2);
                Debug.Log("elese jump");
            }
            else {

                verticalVelocity = -1* gravity * Time.deltaTime *1/4;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        return verticalVelocity;
    }

    private void Interaction()
    {
        //if (!controller.isGrounded)
       // {
        //    Debug.Log("Test Interaction");
        //}

        if(Input.GetKeyDown("e"))
        {
            Debug.Log("E pressed");
        }
    }

    private void Bulle()
    {
    

        if (Input.GetKeyDown("t"))
        {
            Debug.Log("t pressed");
            Instantiate(BulleMesh, transform.position, Quaternion.identity);
            nbreBulle++;
            Create_Bulle.Play();

            Debug.Log(nbreBulle);
        }
    }

    private void InputManagement()
    {
        turnInput = Input.GetAxis("Horizontal");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger!");
        Debug.Log(other.gameObject.tag);

        if(other.gameObject.tag=="Peigne")
        {
            Debug.Log("Peigne");
            Debug.Log("Meurt");
        }
        else if(other.gameObject.tag == "FilElectrique")
        {
            Debug.Log("FilElectrique");
            Debug.Log("Meurt");
        }
        else if (other.gameObject.tag == "EauGoutte")
        {
            Debug.Log("EauGoutte");
            Debug.Log("Meurt");
        }
        else
        {
            Debug.Log("Autre tag");
        }
        //Debug.Log(other.tag);

        //print le type de l'objet
    }

}