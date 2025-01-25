using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[System.Serializable]
public class Level{
    public string name;
    public Texture2D image;
}


/// The game manager
public class GameManagerMain : MonoBehaviour
{
    /// The levels of the game, ordered by difficulty
    public Level[] levels;

    /// The instance of the game manager
    public static GameManagerMain instance { get; private set; } = null;
    public static bool isLoaded { get; private set; } = false;

    /// The current level
    public int currentLevel  = -1;



    #region Unity Methods

    public void Awake() {
        /// If the instance is not set
        if(instance == null) {
            /// Set the instance
            instance = this;
            /// Dont destroy the game manager
            DontDestroyOnLoad(this.gameObject);
            
        } else {
            /// Destroy the game manager
            Destroy(this.gameObject);
        }

        /// Set the instance
        instance = this;
    }

    public void Start() {

        if(!isLoaded) {
            /// Load the game
            Load();
        }
    }


    /// Load the game
    public void Load() {

        /// Load the game
        StartCoroutine(LoadAsync());
    }


    /// Ienumerator to load the game
    public IEnumerator LoadAsync() {

        Debug.Log("Loading game...");


        yield return null;


        /// Set the game as loaded
        isLoaded = true;

        /// Load the first scene
        SceneManager.LoadScene("01_Menu");
    }

    #endregion






    #region Level Loading

    /// Load a level
    public void LoadLevel(int level) {
        /// Check if the level is valid
        if(!isLoaded || level < 0 || level >= levels.Length) {
            Debug.LogError("Invalid level " + level);
            return;
        }

        /// Load the level
        Debug.Log("Loading level " + level);  

        /// Load the scene level
        SceneManager.LoadScene("02_PlayScene");
        currentLevel = level;


        
    }


    public void LoadLevel(string level) {
        for (int i = 0; i < levels.Length; i++) {
            if(levels[i].name == level) {
                LoadLevel(i);
                return;
            }
        }
    }

    public void LoadNextLevel() {
        if(currentLevel < levels.Length - 1)
        {
            LoadLevel(currentLevel + 1);
        }
        else {
            /// Load the final scene
            SceneManager.LoadScene("03_EndScene");
        }
    }


    #endregion

}
