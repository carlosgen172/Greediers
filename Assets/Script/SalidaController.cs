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
        if(textoJugadorGanador != null)
        {
            textoJugadorGanador.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador1") || other.CompareTag("Jugador2"))
        {
            // ocultar al jugar en lugar de eliminarlo
            other.gameObject.SetActive(false);

            // puntaje del otro jugador 
            int puntos = other.GetComponent<JugadorPuntaje>().puntaje;
            puntajes.Add(other.name, puntos);

            jugadoresLlegados++;

            if (jugadoresLlegados == 2)
            {
                DecidirGanador();
            }
        }
    }

    void DecidirGanador()
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

        if (textoJugadorGanador != null)
        {

            textoJugadorGanador.gameObject.SetActive(true);
            textoJugadorGanador.text = "el ganador es " + ganador + " con " + maxPuntaje + " puntos";

            print("el ganador es " + ganador + " con " + maxPuntaje + " puntos");
        }
    }
}
