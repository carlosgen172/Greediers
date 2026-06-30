using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesoroController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) //cuando entra en el area del objeto
    {
        //if (other.CompareTag("Jugador1") || other.CompareTag("Jugador2"))
        if(JuegoManager.Instance.elNombreDelObjeto_SeEncuentraEnLaListaDeNombresDeJugadores(other.gameObject))
        {
            // si el jugadoir está sobre el tesoro
            other.GetComponent<JugadorManager>().estaSobreElTesoro = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) //cuando sale del area del abjeto
    {
        //if (other.CompareTag("Jugador1") || other.CompareTag("Jugador2"))
        if(JuegoManager.Instance.elNombreDelObjeto_SeEncuentraEnLaListaDeNombresDeJugadores(other.gameObject))
        {
            other.GetComponent<JugadorManager>().estaSobreElTesoro = false;
            other.GetComponent<JugadorManager>().DetenerRecoleccion();
        }
    }
}