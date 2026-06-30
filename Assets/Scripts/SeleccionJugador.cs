using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeleccionJugador : MonoBehaviour
{
    JuegoManager gameManager;

    [SerializeField] List<GameObject> listaSeleccionJugadores;
    List<GameObject> listaJugadoresGM; //lista de jugadores que representa la lista del GM

    GameObject jugadorSeleccionado; //se usa para saber que jugador de la lista fue seleccionado


    //se guardan los inputs preseteados en variables de tipo bool
    bool teclaPrimerJugador;
    bool teclaSegundoJugador;
    bool teclaTercerJugador;
    bool teclaCuartoJugador;


    //se verifica que los personajes hayan sido activados por los jugadores
    bool primerPJActivado;
    bool segundoPJActivado;
    bool tercerPJActivado;
    bool cuartoPJActivado;


    //se verifica que los inputs de los jugadores ya fueron seleccionados para el personaje correspondiente
    public bool primerInputSeleccionado;
    public bool segundoInputSeleccionado;
    public bool tercerInputSeleccionado;
    public bool cuartoInputSeleccionado;

    
    int indice; //este índice funciona para actualizar el personaje que pueden seleccionar los jugadores



    void Awake()
    {

    }



    public void Initialization()
    {
        gameManager = JuegoManager.Instance;

        indice = 0;

        listaJugadoresGM = gameManager.listaPrincipalJugadores;
        jugadorSeleccionado = listaJugadoresGM[indice];

        primerPJActivado = false;
        segundoPJActivado = false;
        tercerPJActivado = false;
        cuartoPJActivado = false;

        primerInputSeleccionado = false;
        segundoInputSeleccionado = false;
        tercerInputSeleccionado = false;
        cuartoInputSeleccionado = false;
    }



    void Start()
    {

    }



    void Update()
    {
        //se pueden reconfigurar, las puse por poner basicamente
        teclaPrimerJugador = Input.GetKeyDown(KeyCode.Y);
        teclaSegundoJugador = Input.GetKeyDown(KeyCode.U);
        teclaTercerJugador = Input.GetKeyDown(KeyCode.I);
        teclaCuartoJugador = Input.GetKeyDown(KeyCode.O);

        ActivacionDePersonaje();
    }



    //activa un personaje y determina qué jugador lo usará dependiendo el input que se activó
    private void ActivacionDePersonaje()
    {
        if (!primerPJActivado)
        {
            if (ActivacionDeInput()) primerPJActivado = true;
        }
        else if (!segundoPJActivado)
        {
            if (ActivacionDeInput()) segundoPJActivado = true;
        }
        else if (!tercerPJActivado)
        {
            if (ActivacionDeInput()) tercerPJActivado = true;
        }
        else if (!cuartoPJActivado)
        {
            if (ActivacionDeInput()) cuartoPJActivado = true;
        }
    }

    //activa uno de los inputs disponibles para los jugadores dependiendo la tecla que fué presionada
    private bool ActivacionDeInput()
    {
        if (teclaPrimerJugador && !primerInputSeleccionado)
        {
            AgregarJugadorAListaDeSeleccion();
            primerInputSeleccionado = true;
            Debug.Log("primer input seleccionado: " + primerInputSeleccionado);
            return true;
        }
        else if (teclaSegundoJugador && !segundoInputSeleccionado)
        {
            AgregarJugadorAListaDeSeleccion();
            segundoInputSeleccionado = true;
            Debug.Log("segundo input seleccionado: " + segundoInputSeleccionado);
            return true;
        }
        else if (teclaTercerJugador && !tercerInputSeleccionado)
        {
            AgregarJugadorAListaDeSeleccion();
            tercerInputSeleccionado = true;
            Debug.Log("tercer input seleccionado: " + tercerInputSeleccionado);
            return true;
        }
        else if (teclaCuartoJugador && !cuartoInputSeleccionado)
        {
            AgregarJugadorAListaDeSeleccion();
            cuartoInputSeleccionado = true;
            Debug.Log("cuarto input seleccionado: " + cuartoInputSeleccionado);
            return true;
        }
        return false;
    }

    //agrega un jugador de la lista de jugadores del GM a la lista de selección del menú
    private void AgregarJugadorAListaDeSeleccion()
    {
        if (indice < listaJugadoresGM.Count - 1)
        {
            AgregarJugador_AListaDeSeleccionSiPuede(jugadorSeleccionado);
            indice++;
            jugadorSeleccionado = listaJugadoresGM[indice];
        }
        else
        {
            AgregarJugador_AListaDeSeleccionSiPuede(jugadorSeleccionado);
        }
    }

    private void AgregarJugador_AListaDeSeleccionSiPuede(GameObject player)
    {
        if (listaSeleccionJugadores.Count < listaJugadoresGM.Count)
        {
            listaSeleccionJugadores.Add(player);
        }
    }
}
