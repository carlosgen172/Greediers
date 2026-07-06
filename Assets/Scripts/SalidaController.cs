using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SalidaController : MonoBehaviour

{
    private Dictionary<string, int> puntajes = new Dictionary<string, int>();
    private int jugadoresLlegados = 0;
    public TextMeshProUGUI textoJugadorGanador;

    void Start()
    {
        if (textoJugadorGanador != null)
        {
            textoJugadorGanador.gameObject.SetActive(false);
        }
    }

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

    /*     private void OnTriggerEnter2D(Collider2D other)
        {
            //if (other.CompareTag("Jugador1") || other.CompareTag("Jugador2"))
            if(other.CompareTag("jugador")) //todos los jugadores tendrán el mismo tag.
            {
                // ocultar al jugar en lugar de eliminarlo
                other.gameObject.SetActive(false);

                // puntaje del otro jugador 
                PuntajeJugadorController jugador = other.GetComponent<PuntajeJugadorController>();
                puntajes.Add(jugador.nombreJugador, jugador.puntaje); //Corregirlo por función que guarde en una lista de jugadores dentro del GameManager.

                jugadoresLlegados++;

                if (jugadoresLlegados == 2)
                {
                    DecidirGanador();
                } 
            }
        } */

    /*     void DecidirGanador()
        {
            string ganador = "";
            int maxPuntaje = -1;

            foreach (var valor in puntajes)
            {
                if (valor.Value > maxPuntaje)
                {
                    maxPuntaje = valor.Value;
                    ganador = valor.Key;
                }
            }

            if (textoJugadorGanador != null) //Esta lógica debe estar aislada en en el script que decida 
            {

                textoJugadorGanador.gameObject.SetActive(true);
                textoJugadorGanador.text = "el ganador es " + ganador + " con " + maxPuntaje + " puntos";

                print("el ganador es " + ganador + " con " + maxPuntaje + " puntos");
            }
        } */

    public bool hanSalidoTodosLosJugadores()
    {
        //En caso de que la cantidad de jugadores llegados sea la misma que la cantidad total de los mismos, devolverá true: 
        return jugadoresLlegados == JuegoManager.Instance.listaJugadoresTotales.Count;
    }

}