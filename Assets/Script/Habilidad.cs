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
            JugadorManager gestor = other.GetComponent<JugadorManager>();
            if (gestor != null)
            {
                gestor.ActivarSuperSalto(); 
                //Destroy(gameObject);        
            }
        }
    }
}