using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] List<GameObject> listaSeleccionJugadores;
    List<GameObject> listaJugadoresGM;

    GameObject jugadorSeleccionado;

    int indice;

 

    void Awake()
    {

    }



    public void Initialization()
    {
        gameManager = GameManager.Instance;

        indice = 0;

        //Solamente necesita una lista de jugadores en GameManager
        listaJugadoresGM = gameManager.listaPrincipalJugadores; //El nombre de la lista puede ser modificado a gusto
        jugadorSeleccionado = listaJugadoresGM[indice];
    }



    void Start()
    {

    }



    void Update()
    {

    }



    public void IsButtonPressed()
    {
        if (indice < listaJugadoresGM.Count - 1)
        {
            AñadirPlayer_AListaDeSeleccionSiPuede(jugadorSeleccionado);
            indice++;
            jugadorSeleccionado = listaJugadoresGM[indice];
        }
        else
        {
            AñadirPlayer_AListaDeSeleccionSiPuede(jugadorSeleccionado);
        }
    }

    private void AñadirPlayer_AListaDeSeleccionSiPuede(GameObject player)
    {
        if(listaSeleccionJugadores.Count < listaJugadoresGM.Count)
        {
            listaSeleccionJugadores.Add(player);
        }
    }
}
