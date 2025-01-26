using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private AudioSource Musique_Theme;
    private void Start()
    {
        Musique_Theme.loop = true;  
        Musique_Theme.Play();
    }

    /// Imgui buttons to start a specific level
    public void OnGUI() {
        /// Create a button for each level
        for (int i = 0; i < GameManagerMain.instance.levels.Length; i++) {
            /// Make a 10 * 100 button
            if (GUI.Button(new Rect(10, 10 + i * 20, 100, 15), "Level " + i)) {
                /// Load the level
                GameManagerMain.instance.LoadLevel(i);
            }
        }
    }

}
