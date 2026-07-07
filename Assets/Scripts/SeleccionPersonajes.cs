using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SeleccionPersonajes : MonoBehaviour
{
    JuegoManager gameManager;

    [SerializeField] List<GameObject> SeleccionJugadores;
    List<GameObject> jugadoresDesdeGameManager = new List<GameObject> { }; //lista de jugadores que representa la lista del GM


    // deben asignarse desde el INSPECTOR
    // -----------------------------------------------------------------------------------------------
    public List<TextMeshProUGUI> listaTextosJugadores;
    [SerializeField] List<Image> ImagenesPersonajes;
    // -----------------------------------------------------------------------------------------------


    List<Sprite> ImagenesPersonajesSeleccionados = new List<Sprite> { };
    List<PlayerInput> listaDePlayerInputs = new List<PlayerInput> { };

    GameObject jugadorSeleccionado; //se usa para saber que jugador de la lista fue seleccionado



    //se guardan los inputs preseteados en variables de tipo bool
    bool teclaPrimerJugador, teclaSegundoJugador, teclaTercerJugador, teclaCuartoJugador;

    //se verifica que los personajes hayan sido activados por los jugadores
    bool primerPersonaje, segundoPersonaje, tercerPersonaje, cuartoPersonaje;

    //se verifica que los inputs de los jugadores ya fueron seleccionados para el personaje correspondiente
    public bool primerInput, segundoInput, tercerInput, cuartoInput;


    int indiceJugadoresDesdeGM; //este índice funciona para actualizar el personaje que pueden seleccionar los jugadores
    int indiceTexto; //sirve para actualizar los textos de los jugadores una vez seleccionados
    int indiceImagen; //sirve para actualizar las imagenes de los personajes una vez seleccionados


    void Awake()
    {
        ImagenesPersonajesSeleccionados.AddRange(new List<Sprite> {
            Resources.Load<Sprite>("pablo_seleccion_pj_greediers_selec_finalisimo"),
            Resources.Load<Sprite>("dario_seleccion_pj_greediers_corregido_selec"),
            Resources.Load<Sprite>("mustafa_seleccion_pj_greediers_corregido_selec"),
            Resources.Load<Sprite>("miguel_seleccion_pj_greediers_corregido_selec"),
        });
    }



    public void Initialization()
    {
        gameManager = JuegoManager.Instance;

        indiceJugadoresDesdeGM = 0;
        indiceTexto = 0;
        indiceImagen = 0;

        jugadoresDesdeGameManager = gameManager.listaJugadoresTotales;
        jugadorSeleccionado = jugadoresDesdeGameManager[indiceJugadoresDesdeGM];

        primerPersonaje = false;
        segundoPersonaje = false;
        tercerPersonaje = false;
        cuartoPersonaje = false;

        primerInput = false;
        segundoInput = false;
        tercerInput = false;
        cuartoInput = false;
        for (int i = 0; i < jugadoresDesdeGameManager.Count; i++)
        {
            PlayerInput playerInput;
            playerInput = jugadoresDesdeGameManager[i].GetComponent<PlayerInput>();
            listaDePlayerInputs.Add(playerInput);
        }
    }



    void Start()
    {

    }



    void Update()
    {
        ConfiguracionDeTeclas();
        ActivacionDePersonaje_(ref primerPersonaje);
        ActivacionDePersonaje_(ref segundoPersonaje);
        ActivacionDePersonaje_(ref tercerPersonaje);
        ActivacionDePersonaje_(ref cuartoPersonaje);
    }



    private void ConfiguracionDeTeclas()
    {
        teclaPrimerJugador = false;
        teclaSegundoJugador = false;
        teclaTercerJugador = false;
        teclaCuartoJugador = false;

        if (Keyboard.current != null)
        {
            teclaPrimerJugador = Keyboard.current.wKey.wasPressedThisFrame;
            teclaSegundoJugador = Keyboard.current.upArrowKey.wasPressedThisFrame;
        }

        var gamepads = Gamepad.all;

        if (gamepads.Count > 0)
        {
            teclaTercerJugador = gamepads[0].buttonSouth.wasPressedThisFrame;
        }

        if (gamepads.Count > 1)
        {
            teclaCuartoJugador = gamepads[1].buttonSouth.wasPressedThisFrame;
        }
    }



    //activa un personaje y determina qué jugador lo usará dependiendo el input que se activó
    private void ActivacionDePersonaje_(ref bool unPersonaje)
    {
        if (!unPersonaje && ActivacionDeInput())
        {
            unPersonaje = true;
            CambioDeImagenDePersonaje();
        }
    }


    //activa uno de los inputs disponibles para los jugadores dependiendo la tecla que fué presionada
    private bool ActivacionDeInput()
    {
        return ActivacionDeInputParaJugador(teclaPrimerJugador, ref primerInput, "JP1") ||
            ActivacionDeInputParaJugador(teclaSegundoJugador, ref segundoInput, "JP2") ||
            ActivacionDeInputParaJugador(teclaTercerJugador, ref tercerInput, "JP3") ||
            ActivacionDeInputParaJugador(teclaCuartoJugador, ref cuartoInput, "JP4");
    }


    //activa el input para el jugador unJugador dependiendo la tecla que fué presionada
    private bool ActivacionDeInputParaJugador(bool teclaDeUnJugador, ref bool inputDeUnJugador, string textoUnJugador)
    {
        if (teclaDeUnJugador && !inputDeUnJugador)
        {
            AgregarJugadorAListaDeSeleccion(); //se agrega para que éste NO pueda elegir a otro personaje
            inputDeUnJugador = true; //se activa el input para que NO pueda elegir a otro personaje
            CambioDeTextoParaJugador_(textoUnJugador); //cambia el texto arriba de la imagen del personaje
            return true;
        }
        return false;
    }


    //modifica el texto encima de los personajes dependiendo el input del jugador
    private void CambioDeTextoParaJugador_(string nroJugador)
    {
        if (indiceTexto < listaTextosJugadores.Count)
        {
            listaTextosJugadores[indiceTexto].text = nroJugador;
            indiceTexto++;
        }
    }


    //modifica la imagen del personaje seleccionado
    private void CambioDeImagenDePersonaje()
    {
        if (indiceImagen < ImagenesPersonajes.Count && indiceImagen < ImagenesPersonajesSeleccionados.Count)
        {
            ImagenesPersonajes[indiceImagen].sprite = ImagenesPersonajesSeleccionados[indiceImagen];
            indiceImagen++;
        }
    }


    //agrega un jugador de la lista de jugadores del GM a la lista de selección del menú
    private void AgregarJugadorAListaDeSeleccion()
    {
        if (indiceJugadoresDesdeGM < jugadoresDesdeGameManager.Count - 1)
        {
            AgregarJugador_AListaDeSeleccionSiPuede(jugadorSeleccionado);
            indiceJugadoresDesdeGM++;
            jugadorSeleccionado = jugadoresDesdeGameManager[indiceJugadoresDesdeGM];
        }
        else
        {
            AgregarJugador_AListaDeSeleccionSiPuede(jugadorSeleccionado);
        }
    }


    private void AgregarJugador_AListaDeSeleccionSiPuede(GameObject player)
    {
        if (SeleccionJugadores.Count < jugadoresDesdeGameManager.Count)
        {
            SeleccionJugadores.Add(player);
        }
    }

}
