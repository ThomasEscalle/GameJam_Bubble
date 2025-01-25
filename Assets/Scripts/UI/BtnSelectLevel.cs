using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSelectLevel : MonoBehaviour
{
    public string levelName;
    public bool isUnlocked = true;
    public Text levelText;


    public void Start()
    {
        levelText.text = levelName;
    }

    public void SelectLevel()
    {
        if (isUnlocked)
        {
            GameManagerMain.instance.LoadLevel(levelName);
        }
    }

    public void setLevel(Level level)
    {
        levelName = level.name;
        levelText.text = levelName;
    }
}
