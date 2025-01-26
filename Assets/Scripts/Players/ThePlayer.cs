using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class lastplayer : MonoBehaviour
{
    private Rigidbody rb;

    private float Move;

    public float speed;
    public float jump;

    public float bubleSpawnDistance = 1.0f;

    private bool lastActionIsJump = false;

    private Boolean bDejaTirerBulle;
    public static int nbreBulle;

    [SerializeField] private bool bIsFacingRight = true;

    [Header("References Settings")]
    [SerializeField] protected GameObject BulleMesh;

    [Header("Sound Settings")]
    [SerializeField] private AudioSource Marche_sound;
    [SerializeField] private AudioSource Jump_sound;
    [SerializeField] private AudioSource Create_Bulle_sound;

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



        Interact();
        FaireBulle();
        Move = Input.GetAxis("Horizontal");
        //Marche_sound.Play();
        
        if (Move != 0)
        {
            
            
            lastActionIsJump = false;
            Marche_sound.Stop();

        }
        else
        {
            Marche_sound.Play();
            Marche_sound.loop = true;
            //Debug.Log("audio");
        }


        //rb.velocity = new Vector2(Move * speed, rb.velocity.y);  

        //rb.velocity = new Vector3(Move * speed, rb.velocity.y,0);

        //if (Input.GetButtonDown("Jump") && grounded)
        if (Input.GetButtonDown("Jump") )
        {
            //rb.addForce(new Vector2(Move * speed, jump * 10));

            lastActionIsJump = true;
            //rb.velocity = new Vector2(Move * speed, jump * 5);
            rb.velocity = new Vector3(Move * speed, jump * 5,0);
            Jump_sound.Play();
        }
        else
        {
            rb.velocity = new Vector3(Move * speed, rb.velocity.y, 0);
            //Jump_sound.Stop();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {

        }

        if(other.gameObject.layer == 7)
        {
            Debug.Log("VICOTRY");
            GameManagerMain.instance.LoadNextLevel();
        }


        Vector3 normal = other.GetContact(0).normal;
        if(normal == Vector3.up)
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
        }
        grounded = false;
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

    public void Interact(){
        if (Input.GetButtonDown("Interact"))
        {
            RaycastHit hit;
            Vector3 direction = transform.right;
            if (!bIsFacingRight)
            {
                direction = -transform.right;
            }
            float distance = 2f;


            if (Physics.Raycast(transform.position+ new Vector3(0,0.1f,0), direction, out hit, distance))
            {   
                Debug.Log(hit.transform.gameObject.name);

                Interupteur interupteur = hit.transform.gameObject.GetComponent<Interupteur>();
                if(interupteur != null) {
                    interupteur.trigger();
                }
                
            }
            else if (Physics.Raycast(transform.position - new Vector3(0,0.1f,0), direction, out hit, distance))
            {
                Debug.Log(hit.transform.gameObject.name);
                Interupteur interupteur = hit.transform.gameObject.GetComponent<Interupteur>();
                if(interupteur != null) {
                    interupteur.trigger();
                }
            }

            Debug.DrawRay(transform.position, direction * distance  , Color.red, 1.0f);
        }
    }


    private void FaireBulle()
    {

        if ((Input.GetButtonDown("MakeBulle")) && nbreBulle < 3 && !Input.GetKey(KeyCode.W))
        {

            /// Create the position and the rotation of the bubble
            Vector3 spawnPosition = transform.position;
            Vector3 spawnDirection = transform.right;
            Create_Bulle_sound.Play();
               
            if (bIsFacingRight)
            {
                spawnPosition += transform.right * bubleSpawnDistance;
                
            }
            else
            {
                spawnPosition -= transform.right * bubleSpawnDistance;
                spawnDirection = -transform.right;
            }

            GameObject bulle = Instantiate(BulleMesh, spawnPosition, Quaternion.identity);
            nbreBulle++;

            Bubble bubbleScript = bulle.GetComponent<Bubble>();
            bubbleScript.direction = spawnDirection;
            bubbleScript.disiredDirection = spawnDirection;
            bubbleScript.speed = 5.0f;
        }
        //// Si the player a en meme temps "Z" et "MakeBulle" on tire une bulle vers le haut
        else if ((Input.GetButtonDown("MakeBulle")) && Input.GetKey(KeyCode.W) && nbreBulle < 3)
        {
            /// Tire une bulle vers le haut
            Vector3 spawnPosition = transform.position + Vector3.up * bubleSpawnDistance;
            Vector3 spawnDirection = Vector3.up;

            GameObject bulle = Instantiate(BulleMesh, spawnPosition, Quaternion.identity);
            nbreBulle++;

            Bubble bubbleScript = bulle.GetComponent<Bubble>();
            bubbleScript.direction = spawnDirection;
            bubbleScript.disiredDirection = spawnDirection;
            bubbleScript.speed = 5.0f;
        }

    }


}
