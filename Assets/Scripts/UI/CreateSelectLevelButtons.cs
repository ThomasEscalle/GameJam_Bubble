using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSelectLevelButtons : MonoBehaviour
{
    public GameObject buttonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < GameManagerMain.instance.levels.Length; i++)
        {
            GameObject button = Instantiate(buttonPrefab, transform);
            button.GetComponent<BtnSelectLevel>().setLevel(GameManagerMain.instance.levels[i]);
        }    
    }
}
