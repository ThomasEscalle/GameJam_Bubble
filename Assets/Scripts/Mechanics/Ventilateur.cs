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

    public int EventChannel;

    public void Start()
    {
        /// Set the range of the ventilator.
        if (collider != null)
        {
            Debug.Log("Collider found");
            if (direction == Direction.Up)
            {
                collider.size = new Vector3(1, range, 1);
                collider.center = new Vector3(0, (range / 2)+0.5f, 0);
            }
            else if (direction == Direction.Down)
            {
                collider.size = new Vector3(1, range, 1);
                collider.center = new Vector3(0, (-range / 2)-0.5f, 0 );
            }
            else if (direction == Direction.Left)
            {
                collider.size = new Vector3(range, 1, 1);
                collider.center = new Vector3((-range / 2) - 0.5f, 0, 0);
            }
            else if (direction == Direction.Right)
            {
                collider.size = new Vector3(range, 1, 1);
                collider.center = new Vector3((range / 2) + 0.5f, 0, 0);
            }
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bubble"))
        {
            Bubble bubble = other.GetComponent<Bubble>();
            if (bubble != null)
            {
                if (direction == Direction.Up)
                {
                    bubble.disiredDirection = Vector3.up;
                }
                else if (direction == Direction.Down)
                {
                    bubble.disiredDirection = Vector3.down;
                }
                else if (direction == Direction.Left)
                {
                    bubble.disiredDirection = Vector3.left;
                }
                else if (direction == Direction.Right)
                {
                    bubble.disiredDirection = Vector3.right;
                }
            }
        }
    }

}
