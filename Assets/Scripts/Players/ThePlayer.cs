using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class lastplayer : MonoBehaviour
{
    private Rigidbody rb;

    private float Move;

    public float speed;
    public float jump;

    private Boolean bDejaTirerBulle;
    private int nbreBulle;

    [SerializeField] private bool bIsFacingRight = true;

    [Header("References Settings")]
    [SerializeField] protected GameObject BulleMesh;

    [Header("Sound Settings")]
    [SerializeField] private AudioSource Marche;
    [SerializeField] private AudioSource Jump;
    [SerializeField] private AudioSource Create_Bulle;


    bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();

        Interaction();

        FaireBulle();
        Move = Input.GetAxis("Horizontal");

        //rb.velocity = new Vector2(Move * speed, rb.velocity.y);  

        //rb.velocity = new Vector3(Move * speed, rb.velocity.y,0);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            //rb.addForce(new Vector2(Move * speed, jump * 10));


            //rb.velocity = new Vector2(Move * speed, jump * 5);
            rb.velocity = new Vector3(Move * speed, jump * 5,0);
        }
        else
        {
            rb.velocity = new Vector3(Move * speed, rb.velocity.y, 0);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            Vector3 normal = other.GetContact(0).normal;
            if(normal == Vector3.up)
            {
                grounded = true;
            }


            
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    private void Flip()
    {
        if ((bIsFacingRight && Move < 0f) || (!bIsFacingRight && Move > 0f))
        {
            bIsFacingRight = !bIsFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void Interaction()
    {
        //if (!controller.isGrounded)
        // {
        //    Debug.Log("Test Interaction");
        //}

        if (Input.GetButtonDown("Interact"))
        {
            Debug.Log("Interact:on a appuyer sur e, on peut interragir");
        }
    }

    private void FaireBulle()
    {
        if ((Input.GetButtonDown("MakeBulle")) && nbreBulle <=3)
        {
            Debug.Log("MakeBulle:on a appuyer sur t, on peut creer bulle");
            Instantiate(BulleMesh, transform.position, Quaternion.identity);
            nbreBulle++;
            Create_Bulle.Play();

            Debug.Log("On a ce nombre de bulles:");
            Debug.Log(nbreBulle);
        }
    }
}
