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

    void Awake()
    {

    }
    void Start()
    {
        
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
        textoJugador1 = GameObject.Find("textoPuntuacion1").GetComponent<TextMeshProUGUI>();
        textoJugador2 = GameObject.Find("textoPuntuacion2").GetComponent<TextMeshProUGUI>();
        textoJugador3 = GameObject.Find("textoPuntuacion3").GetComponent<TextMeshProUGUI>();
        textoJugador4 = GameObject.Find("textoPuntuacion4").GetComponent<TextMeshProUGUI>();
        textoJugador1.text = "0";
        textoJugador2.text = "0";
        textoJugador3.text = "0";
        textoJugador4.text = "0";
    }

}