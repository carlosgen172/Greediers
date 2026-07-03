using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //para mostrar los puntajes provisoriamente
    public TextMeshProUGUI textoJugador1; //El gameobject de esto se cambio por el texto que tiene cada jugador dentro del inspector.
    public TextMeshProUGUI textoJugador2;
    public TextMeshProUGUI textoJugador3;
    public TextMeshProUGUI textoJugador4;
    
    /* 
        [Serializable]
        public class JugadorTexto
        {
            public JugadorPuntaje valores;
            public TextMeshProUGUI textoUI;

        } */

    void Start()
    {
        InicializarTextosDePuntuacion();
    }

    public void ActualizarPuntaje(PuntajeJugadorController jugadorPuntaje)
    {
        //if (jugadorPuntaje.CompareTag("Jugador_1"/* "Jugador1" */))
        if (jugadorPuntaje.nombreJugador == "Jugador_1")
        {
            textoJugador1.text = jugadorPuntaje.nombreJugador + ": " + jugadorPuntaje.puntaje;
        }

        if (jugadorPuntaje.nombreJugador == "Jugador_2")
        {
            textoJugador2.text = jugadorPuntaje.nombreJugador + ": " + jugadorPuntaje.puntaje;
        }

        if (jugadorPuntaje.nombreJugador == "Jugador_3")
        {
            textoJugador3.text = jugadorPuntaje.nombreJugador + ": " + jugadorPuntaje.puntaje;
        }

        if (jugadorPuntaje.nombreJugador == "Jugador_4")
        {
            textoJugador4.text = jugadorPuntaje.nombreJugador + ": " + jugadorPuntaje.puntaje;
        }

/*         if (jugadorPuntaje.CompareTag("Jugador2"))
        {
            textoJugador2.text = jugadorPuntaje.nombreJugador + ": " + jugadorPuntaje.puntaje;
        } */

    }

    /*         foreach (var jugador in listaDeJugadores)
            {
                if (jugador.valores == jugadorPuntaje)
                {
                    jugador.textoUI.text = jugadorPuntaje.nombreJugador + ": " + jugadorPuntaje.puntaje + " pts";
                    break;
                }
            } */


    public void InicializarTextosDePuntuacion()
    {
        var jugador_1 = GameObject.Find("Jugador_1");
        textoJugador1 = jugador_1.GetComponentInChildren<TextMeshProUGUI>();
        textoJugador1.text = "0";

        /* DESCOMENTAR CUANDO SE CREEN BIEN TODOS LOS PERSONAJES.

        var jugador_2 = GameObject.Find("Jugador_2");
        textoJugador2 = jugador_2.GetComponentInChildren<TextMeshProUGUI>();
        textoJugador2.text = "0";

        var jugador_3 = GameObject.Find("Jugador_3");
        textoJugador3 = jugador_3.GetComponentInChildren<TextMeshProUGUI>();
        textoJugador3.text = "0";

        var jugador_4 = GameObject.Find("Jugador_4");
        textoJugador4 = jugador_4.GetComponentInChildren<TextMeshProUGUI>();
        textoJugador4.text = "0";
        */
    }

}