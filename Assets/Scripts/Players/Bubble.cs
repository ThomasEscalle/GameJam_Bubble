using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for the bubble object.
/// </summary>
public class Bubble : MonoBehaviour
{

    /// Direction of the bubble object.
    public Vector3 direction = Vector3.right;
    public Vector3 disiredDirection = Vector3.right;
    public float timeToChangeDirection = 1.0f;

    /// Speed of the bubble object.
    public float speed = 5.0f;

    /// The force of the bubble object (goind down with time)
    public float force = 1.0f;

    /// The offset of the bubbleObject, used to animate the bubble object with a sinusoide.
    public Transform bubbleOffset;


    [Header("Life Time")]
    public float lifeTime = 5.0f;
    public float currentTime = 0.0f;

    [Header("Animation")]
    public float amplitude = 0.1f;
    public float frequency = 10.0f;
    
    public void Update()
    {   

        /// Make the direction of the bubble near the desired direction.
        direction = Vector3.Lerp(direction, disiredDirection, Time.deltaTime / timeToChangeDirection);

        /// Transform the bubble object to the direction and speed.
        transform.position = transform.position + (direction * speed * Time.deltaTime);

        /// Update the time
        currentTime += Time.deltaTime;
        if (currentTime >= lifeTime)
        {
            DestroySelf();
        }

        /// Animate the bubble object with a sinusoide.
        if (bubbleOffset != null)
        {
            //bubbleOffset.localPosition = new Vector3(0, Mathf.Sin(Time.time * frequency) * amplitude, 0);
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        /// If the bubble object enters the collider, destroy the bubble object.
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player"))
        {
            DestroySelf();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        /// If the bubble object exits the collider, destroy the bubble object.
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player"))
        {
            DestroySelf();
        }
    }


    public void DestroySelf()
    {
        /// Destroy the bubble object.
        Destroy(gameObject);
        lastplayer.nbreBulle--;
    }


}
