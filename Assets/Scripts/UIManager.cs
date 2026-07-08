using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Puntajes de cada jugador")]
    public TextMeshProUGUI textoJugador1;
    public TextMeshProUGUI textoJugador2;
    public TextMeshProUGUI textoJugador3;
    public TextMeshProUGUI textoJugador4;

    public void ActualizarPuntaje(PuntajeJugadorController jugadorPuntaje)
    {
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
    }

    public void ActualizarTextoDelJugador(GameObject unJugador)
    {
        var puntajeDeJugadorAModificar = unJugador.GetComponent<PuntajeJugadorController>();
        if (unJugador.name == "Jugador_1")
        {
            textoJugador1.text = unJugador.name + ": " + puntajeDeJugadorAModificar.puntaje;
        }
        if (unJugador.name == "Jugador_2")
        {
            textoJugador1.text = unJugador.name + ": " + puntajeDeJugadorAModificar.puntaje;
        }
        if (unJugador.name == "Jugador_3")
        {
            textoJugador1.text = unJugador.name + ": " + puntajeDeJugadorAModificar.puntaje;
        }
        if (unJugador.name == "Jugador_4")
        {
            textoJugador1.text = unJugador.name + ": " + puntajeDeJugadorAModificar.puntaje;
        }
    }

    // public void ActualizarPuntajeEnBaseAunEntero(int unInt)
    // {
    //     if()
    // }

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