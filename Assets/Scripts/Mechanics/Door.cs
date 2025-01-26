using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Lean tween

public class Door : MonoBehaviour
{
    public int eventChannel = 0;

    public Transform doorAnimated;

    public Vector3 opendRotation;
    public Vector3 closedRotation;


    /// The collider of the door
    public Collider doorCollider;

    public void Start() {
        /// Set the door to closed
        doorAnimated.rotation = Quaternion.Euler(opendRotation);
    }

    public void trigger() {
        Debug.Log("Triggering door on channel " + eventChannel);
        open();
    }

    public void open() {
        Debug.Log("Opening door");  

        LeanTween.rotate(doorAnimated.gameObject, closedRotation    , 1f).setEase(LeanTweenType.easeOutBack);
        /// Disable the collider
        doorCollider.enabled = false;

    }

    public void close() {
        LeanTween.rotate(doorAnimated.gameObject, opendRotation, 1f).setEase(LeanTweenType.easeOutBack);
        /// Enable the collider
        doorCollider.enabled = true;
    }

}
