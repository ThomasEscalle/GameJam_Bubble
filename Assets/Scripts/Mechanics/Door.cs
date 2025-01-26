using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int eventChannel = 0;

    public void trigger() {
        Debug.Log("Door triggered");
    }
}
