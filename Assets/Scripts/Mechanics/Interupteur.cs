using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// L'interupteur permet d'activer des objets avec la main du joueur
public class Interupteur : MonoBehaviour
{
    public int eventChannel = 0;
    private bool On = true;
    [SerializeField] private Ventilateur SCR_ventilateur;
    [SerializeField] private BoxCollider Vent_Ventilo;



    public void trigger() {
        Debug.Log("Triggering event on channel " + eventChannel);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bubble")) {
            if (SCR_ventilateur.EventChannel == eventChannel)
            {
                Debug.Log(" Lien retrouvé");
            }
            else
            {
                Debug.Log("Lien pas retrouvé");
            }
            if (On)
            {
                Vent_Ventilo.enabled = false;
                On = false;
                Debug.Log("eteint");
            }
            else
            {
                Vent_Ventilo.enabled = true;
                On = true;
                Debug.Log("Allumé");
            }

        }
    }

}
