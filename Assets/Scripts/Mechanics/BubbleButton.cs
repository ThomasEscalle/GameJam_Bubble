using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleButton : MonoBehaviour
{
    public int eventChannel = 0;
    public void trigger()
    {
        Debug.Log("Triggering event on channel " + eventChannel);
        /// Get all the "Door" objects in the scene
        Door[] doors = FindObjectsOfType<Door>();
        foreach (Door door in doors)
        {
            if (door.eventChannel == eventChannel)
            {
                door.trigger();
            }
        }
    }
}
