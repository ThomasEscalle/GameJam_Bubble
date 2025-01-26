using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// L'interupteur permet d'activer des objets avec la main du joueur
public class Interupteur : MonoBehaviour
{
    public int eventChannel = 0;

    public void trigger() {
        Debug.Log("Door triggered");
    }

}
