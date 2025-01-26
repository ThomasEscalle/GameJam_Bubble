using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleButton : MonoBehaviour
{
    public int eventChannel = 0;
    public void trigger()
    {
        Debug.Log("Triggering event on channel " + eventChannel);
    }
}
