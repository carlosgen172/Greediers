using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Spawn : MonoBehaviour
{

    [Header("Esquinas a spawnear:")]
    public GameObject esquinaNoreste;
    public GameObject esquinaNoroeste;
    public GameObject esquinaSureste;
    public GameObject esquinaSuroeste;

    Vector2 posNoreste;
    Vector2 posNoroeste;
    Vector2 posSureste;
    Vector2 posSuroeste;

    List<Vector2> posiciones;
    List<Vector2> posDisponibles;

    int indice;

    //El spawneo del sprite del personaje dependerá de donde spawnee.
    // (ejemplo: No será la misma dirección de vista del pj que spawneo a la izq 
    // que el que spawneo a la izq, ver de usar función de giro de pj incluido 
    // en el movementJugador comparándolo quizás con los prefabs de las posiciones, 
    // dependiendo que prefab ha sido elegido, el "flip" que realizará el pj. 
    // Se puede usar función de "Inicializar()" dentro del JugadorManager)


    //inicializa las variables para dejar la función eleccionDePosicion() listo para su funcionamiento
    public void Initialization()
    {
        // instanciar listas antes de usarlas
        posiciones = new List<Vector2>();
        posDisponibles = new List<Vector2>();

        seteoPosiciones();

        posiciones.AddRange(new List<Vector2>{posNoreste, posNoroeste, posSureste, posSuroeste});
        posDisponibles.AddRange(posiciones);

        indice = 0;
    }

    //setea las posiciones de los prefabs en variables de tipo vector2 para un mejor manejo
    private void seteoPosiciones()
    {
        posNoreste = esquinaNoreste.transform.position;
        posNoroeste = esquinaNoroeste.transform.position;
        posSureste = esquinaSureste.transform.position;
        posSuroeste = esquinaSuroeste.transform.position;
    }

    public Vector2 eleccionDePosicion()
    {
        //el indice de la lista elegido de forma random. Éste usa enteros, incluye el valor min pero no el valor max
        indice = Random.Range(0, posDisponibles.Count);

        //posicion elegida segun el índice en la lista de posiciones disponibles
        Vector2 posSeleccionada = posDisponibles[indice];

        //remueve la posición seleccionada para poder elegir una de las restantes
        posDisponibles.RemoveAt(indice);

        return posSeleccionada;
    }
}