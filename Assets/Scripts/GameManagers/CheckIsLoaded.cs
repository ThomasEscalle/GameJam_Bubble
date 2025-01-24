using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckIsLoaded : MonoBehaviour
{

    /// Check if the game manager is loaded
    public void Awake() {
        if(!GameManagerMain.isLoaded) {
            SceneManager.LoadScene(0);
        }
    }


}
