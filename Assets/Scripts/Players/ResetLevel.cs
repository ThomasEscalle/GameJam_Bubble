using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLevel : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            GameManagerMain.instance.ResetLevel();
        }
    }
}
