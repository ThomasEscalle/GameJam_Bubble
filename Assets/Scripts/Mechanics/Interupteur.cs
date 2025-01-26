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
    [SerializeField] private ParticleSystem Particule;
    [SerializeField] AudioSource Audio_allume;
    [SerializeField] AudioSource Audio_eteint;



    public void trigger() {
        Debug.Log("Triggering event on channel " + eventChannel);

        /// Get all the "Ventilateur" objects in the scene, and trigger them if they have the same event channel
        Ventilateur[] ventilateurs = FindObjectsOfType<Ventilateur>();
        foreach (Ventilateur ventilateur in ventilateurs) {
            if (ventilateur.EventChannel == eventChannel) {
                ventilateur.trigger();

                /*
                // Particule.Play();
                if (On)
                {
                    Audio_eteint.Play();
                }
                else
                {
                    Audio_allume.Play();
                }*/
            }
        }
    }


    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bubble")) {
            if (SCR_ventilateur.EventChannel == eventChannel)
            {
                Debug.Log(" Lien retrouv�");
            }
            else
            {
                Debug.Log("Lien pas retrouv�");
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
                Debug.Log("Allum�");
            }

        }
    }
    */

}
