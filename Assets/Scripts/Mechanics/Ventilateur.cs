using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ventilateur : MonoBehaviour
{

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    /// The direction of the ventilator.
    public Direction direction = Direction.Up;
    /// The range of the ventilator.
    public float range = 8.0f;

    /// The collider of the ventilator (where the bubble object will be affected).
    public BoxCollider collider;

    public void Start()
    {
        /// Set the range of the ventilator.
        BoxCollider collider = GetComponent<BoxCollider>();
        if (collider != null)
        {
            collider.size = new Vector3(1, range, 1);
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bubble"))
        {
            Bubble bubble = other.GetComponent<Bubble>();
            if (bubble != null)
            {
                Debug.Log("Ventilateur");
                bubble.disiredDirection = Vector3.up;
            }
        }
    }

}
