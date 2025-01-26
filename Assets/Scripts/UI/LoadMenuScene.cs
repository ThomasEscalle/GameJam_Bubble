using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMenuScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       /// Load the menu scene after 8 seconds
         StartCoroutine(LoadMenu()); 
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("01_Menu");
        }
    }

    IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(8);
        UnityEngine.SceneManagement.SceneManager.LoadScene("01_Menu");
    }
}
