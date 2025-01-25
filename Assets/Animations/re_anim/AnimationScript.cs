using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log(animator);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            Debug.Log("w appuyer");
            animator.SetBool("isWalking", true);
            
        }

    }
}
