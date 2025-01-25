using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings ")]
    [SerializeField] private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private bool bIsFacingRight = true;
    [SerializeField] private bool bIsGrounded = false;

    [Header("References")]
    protected CharacterController controller;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject groundCheckObject;
    [SerializeField] private LayerMask groundLayer;


    
    
    // Start is called before the first frame update



    void Start()
    {
        //controller = GetComponent<CharacterController>();
        //groundCheckCollider = groundCheckObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {


        InputManagement();


        JumpCalc();
        

        Flip();

        /*Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;*/


        //att

        // Utiliser un OnCollisionStay2D ou OnCollisionEnter2D sur le BoxCollider
        // pour détecter si l'enfant est en contact avec un objet qui représente le sol.
        
        //CheckIfGrounded();
    }

    private void InputManagement()
    {
        horizontal = Input.GetAxis("Horizontal");
    }


    private void JumpCalc()
    {
        //if (Input.GetButtonDown("Jump") && isGrounded())
        if (Input.GetButtonDown("Jump") && bIsGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonDown("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

    }

    /*private void CheckIfGrounded()
    {
        Collider[] hitColliders= Physics2D.OverlapBoxAll(groundCheckCollider.bounds.center, groundCheckCollider.bounds.size, 0f);

        isGrounded = false;
    
        
    }*/

    
    private void OnCollisionEnter(Collision other)
    {
        /*
        if (other.gameObject.CompareTage("Ground"))
        {
            bIsGrounded = true;
        }
        else
        {
            Debug.Log("Pas au sol");
        }
        */

        Debug.Log("Pas au sol");
    }


    /*private bool isGrounded()
    {

        //return Physics.OverlapCirle(groundCheck.position, 0.2f, groundLayer);
        //get groundcheck collision

        
        if(groundCheckObject)
        return true;
    }*/


    private void EdgeDectetionc()
    {
        //horizontal = Input.GetAxis("Horizontal");
        Debug.Log("test");
    }

    private void FixedUpdate()
    {
        //transform.position.z = 0;
        rb.velocity = new Vector3(horizontal * speed, rb.velocity.y,0);
    }

    private void Flip()
    {
        if((bIsFacingRight && horizontal<0f) || (!bIsFacingRight && horizontal > 0f))
        {
            bIsFacingRight = !bIsFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
