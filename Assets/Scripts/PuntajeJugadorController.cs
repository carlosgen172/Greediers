using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntajeJugadorController : MonoBehaviour
{

    [Header("Puntuación actual:")]
    public int puntaje; 

    [Header("Nombre del jugador:")]
    public string nombreJugador;

    void Start()
    {
        //Se inicializa el puntaje
        puntaje = 0;

        //El nombre del jugador se iguala al nombre de su gameObject.
        nombreJugador = gameObject.name;
    
    }

    //Función para perder una cantidad determinada de tesoro (activada por las vendas enemigas):
    public void PerderTesoro(int unaCantidadDeTesoro)
    {
        puntaje = Mathf.Max(0, puntaje - unaCantidadDeTesoro);
    }

}
