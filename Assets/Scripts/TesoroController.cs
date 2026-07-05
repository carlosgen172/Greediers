using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TesoroController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("jugador"))
        {
            // TryGetComponent es más eficiente y seguro
            if (other.TryGetComponent<JugadorManager>(out JugadorManager manager))
            {
                manager.estaSobreElTesoro = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("jugador"))
        {
            if (other.TryGetComponent<JugadorManager>(out JugadorManager manager))
            {
                manager.estaSobreElTesoro = false;
                manager.DetenerRecoleccion();
            }
        }
    }
}