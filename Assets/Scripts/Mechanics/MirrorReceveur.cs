using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorReceveur : MonoBehaviour
{

    /// The time since the last trigger
    private float timeSinceLastTrigger = 0;
    private float timeToWait = 1.3f;

    /// The event channel to trigger to 
    public int eventChannel = 0;

    public void Update()
    {
        timeSinceLastTrigger += Time.deltaTime;
    }

    public void trigger() {
        if (timeSinceLastTrigger > timeToWait) {
            timeSinceLastTrigger = 0;
            
            /// Get all the "Door" objects in the scene
            Door[] doors = FindObjectsOfType<Door>();
            foreach (Door door in doors) {
                if (door.eventChannel == eventChannel) {
                    door.trigger();
                }
            }
        }
    }

}
