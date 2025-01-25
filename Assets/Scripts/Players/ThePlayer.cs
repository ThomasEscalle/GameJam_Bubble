using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lastplayer : MonoBehaviour
{
    private Rigidbody rb;

    private float Move;

    public float speed;
    public float jump;

    bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
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
}
