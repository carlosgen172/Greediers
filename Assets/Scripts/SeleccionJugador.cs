using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SeleccionJugador : MonoBehaviour
{
    JuegoManager gameManager;

    [SerializeField] List<GameObject> SeleccionJugadores;
    List<GameObject> listaJugadoresGM = new List<GameObject> {}; //lista de jugadores que representa la lista del GM


    // deben asignarse desde el INSPECTOR
    // -----------------------------------------------------------------------------------------------
    [SerializeField] List<TextMeshProUGUI> listaTextosJugadores;
    [SerializeField] List<Image> ImagenesPersonajes;
    // -----------------------------------------------------------------------------------------------


    List<Sprite> ImagenesPersonajesSeleccionados = new List<Sprite> {};

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
    int indiceTexto; //sirve para actualizar los textos de los jugadores una vez seleccionados
    int indiceImagen; //sirve para actualizar las imagenes de los personajes una vez seleccionados


    void Awake()
    {
        ImagenesPersonajesSeleccionados.AddRange(new List<Sprite> {
            Resources.Load<Sprite>("pablo_seleccion_pj_greediers_selec_finalisimo"),
            Resources.Load<Sprite>("dario_seleccion_pj_greediers_corregido_selec"),
            Resources.Load<Sprite>("mustafa_seleccion_pj_greediers_corregido_selec"),
            Resources.Load<Sprite>("miguel_seleccion_pj_greediers_corregido_selec")
        });
    }



    public void Initialization()
    {
        gameManager = JuegoManager.Instance;

        indice = 0;
        indiceTexto = 0;
        indiceImagen = 0;

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
        ConfiguracionDeTeclas();
        ActivacionDePersonaje();
    }



    private void ConfiguracionDeTeclas()
    {
        if(Keyboard.current != null)
        {
            teclaPrimerJugador = Keyboard.current.zKey.wasPressedThisFrame;
            teclaSegundoJugador = Keyboard.current.commaKey.wasPressedThisFrame;
        }
        if(Gamepad.current != null)
        {
            teclaTercerJugador = Gamepad.current.buttonSouth.wasPressedThisFrame;
            teclaCuartoJugador = Gamepad.current.buttonSouth.wasPressedThisFrame;
        }
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
            CambioDeTextoParaJugador_("JP1");
            CambioDeImagenDePersonaje();
            return true;
        }
        else if (teclaSegundoJugador && !segundoInputSeleccionado)
        {
            AgregarJugadorAListaDeSeleccion();
            segundoInputSeleccionado = true;
            CambioDeTextoParaJugador_("JP2");
            CambioDeImagenDePersonaje();
            return true;
        }
        else if (teclaTercerJugador && !tercerInputSeleccionado)
        {
            AgregarJugadorAListaDeSeleccion();
            tercerInputSeleccionado = true;
            CambioDeTextoParaJugador_("JP3");
            CambioDeImagenDePersonaje();
            return true;
        }
        else if (teclaCuartoJugador && !cuartoInputSeleccionado)
        {
            AgregarJugadorAListaDeSeleccion();
            cuartoInputSeleccionado = true;
            CambioDeTextoParaJugador_("JP4");
            CambioDeImagenDePersonaje();
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
        if (indiceImagen < ImagenesPersonajes.Count)
        {
            if (indiceImagen == 0)
            {
                ImagenesPersonajes[indiceImagen].sprite = ImagenesPersonajesSeleccionados[0];
            }
            else if (indiceImagen == 1)
            {
                ImagenesPersonajes[indiceImagen].sprite = ImagenesPersonajesSeleccionados[1];
            }
            else if (indiceImagen == 2)
            {
                ImagenesPersonajes[indiceImagen].sprite = ImagenesPersonajesSeleccionados[2];
            }
            else if (indiceImagen == 3)
            {
                ImagenesPersonajes[indiceImagen].sprite = ImagenesPersonajesSeleccionados[3];
            }
            indiceImagen++;
        }
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
        if (SeleccionJugadores.Count < listaJugadoresGM.Count)
        {
            SeleccionJugadores.Add(player);
        }
    }

}
