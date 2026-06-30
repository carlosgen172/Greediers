using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //para mostrar los puntajes provisoriamente
    public TextMeshProUGUI textoJugador1;
    public TextMeshProUGUI textoJugador2;
    /* 
        [Serializable]
        public class JugadorTexto
        {
            public JugadorPuntaje valores;
            public TextMeshProUGUI textoUI;

        } */

    void Start()
    {

        textoJugador1.text = "0";
        textoJugador2.text = "0";
    }

    public void ActualizarPuntaje(PuntajeJugadorController jugadorPuntaje)
    {
        if (jugadorPuntaje.CompareTag("Jugador1"))
        {
            textoJugador1.text = jugadorPuntaje.nombreJugador + ": " + jugadorPuntaje.puntaje;
        }

        if (jugadorPuntaje.CompareTag("Jugador2"))
        {
            textoJugador2.text = jugadorPuntaje.nombreJugador + ": " + jugadorPuntaje.puntaje;
        }

    }
    /*         foreach (var jugador in listaDeJugadores)
            {
                if (jugador.valores == jugadorPuntaje)
                {
                    jugador.textoUI.text = jugadorPuntaje.nombreJugador + ": " + jugadorPuntaje.puntaje + " pts";
                    break;
                }
            } */

}