using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidad : MonoBehaviour
{
    /* private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Jugador1"))
            {
                other.GetComponent<JugadorManager>().ActivarSuperSalto();
                //Destroy(gameObject); 
            }
        }
    } */

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador1"))
        {
            JugadorManager JGManager = other.GetComponent<JugadorManager>();
            if (JGManager != null)
            {
                JGManager.ActivarHabilidad();
                //Destroy(gameObject); 
            }
        }
    }
}