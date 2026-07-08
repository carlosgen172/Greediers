using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SalidaController : MonoBehaviour

{
    // Valores para la lógica de guardado de puntajes
    private int jugadoresLlegados = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("jugador"))
        {

            PuntajeJugadorController jugador = other.GetComponent<PuntajeJugadorController>();

            if (!JuegoManager.Instance.jugadoresQueLlegaron.Contains(jugador))
            {
                JuegoManager.Instance.jugadoresQueLlegaron.Add(jugador);
            }

            other.gameObject.SetActive(false);
            jugadoresLlegados++;

            
        }
    }

    public bool hanSalidoTodosLosJugadores()
    {
        //En caso de que la cantidad de jugadores llegados sea la misma que la cantidad total de los mismos, devolverá true: 
        return jugadoresLlegados == JuegoManager.Instance.listaJugadoresTotales.Count;
    }

}