using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class BotonesManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Valores referidos al manejo de cambio de escenas: ")]
    public string nombreBoton;
    public string escenaADirigir;

    [Header("Valores referidos al manejo de los sonidos de los botones al clickear:")]
    public AudioClip SonidoBotonAbajo;
    public AudioClip SonidoBotonArriba;

    [Header("Acción Personalizada del Botón")]
    // Esto aparecerá en el inspector de Unity como el clásico "On Click()"
    public UnityEvent accionAlSoltar;

    void Awake()
    {
        //Guardo el nomnbre del boton para qe el mismo 
        //sea el que defina su funcionalidad.

        nombreBoton = gameObject.name;

        SonidoBotonAbajo = Resources.Load<AudioClip>("sfx_roca_1_5");

        SonidoBotonArriba = Resources.Load<AudioClip>("sfx_explosion_2");

    }

    // Start is called before the first frame update
    void Start()
    {
        InicializarBoton();
    }

    //Funciones generales de cualquier botón:


    //Función de pre-configuración del botón:

    private void InicializarBoton()
    {
        SetearEscenaADirigirSiCorresponde();

        CambiarTextoDeBotonSiCorresponde();
    }

    //Funciones generales para su inicialización:

    private void SetearEscenaADirigirSiCorresponde()
    {
        //Seteo de escena a dirigir en base a su nombre:

        if(nombreBoton == "BotonPlay")
        {
            escenaADirigir = "MenuSeleccionPJ";

        } else if(nombreBoton == "BotonSalirAlMenu" || nombreBoton == "BotonVolverAlMenu")
        {
            escenaADirigir = "MenuPrincipal";

        } else if(nombreBoton == "BotonOptions")
        {
            escenaADirigir = "MenuOpciones";

        }else if(nombreBoton == "BotonTutorial") 
        {
            escenaADirigir = "MenuTutorial";

        } else if(nombreBoton == "BotonIrAJugar") //PARA PRUEBA (MODIFICAR CUANDO ESTÉ LISTO EL SISTEMA DE SELECCIÓN DE PJS POR INPUT CONCRETADO)
        {
            escenaADirigir = "Nivel";
        }

    }

    //Función general para el manejo de cambio de escenas:

    public void CambiarAEscenaCorrespondiente()
    {
        SceneManager.LoadScene(escenaADirigir);
    }

    //Funciones generales para la activación de efectos de sonido y acciones en base a su interacción:

    //Función ejecutable cuando el boton es presionado:
    public void OnPointerDown(PointerEventData eventData) //pointerEventData: instancia de elemento referido a los eventos de interfaz, que se genera cada que se cumple esta acción, guardando toda la información relevante al botón.
    {
        AudioManager.Instance.ReproducirSonido(SonidoBotonAbajo, 1);
        
    }

    //Función ejecutable cuando el boton es soltado:
    public void OnPointerUp(PointerEventData eventData)
    {
        AudioManager.Instance.ReproducirSonido(SonidoBotonArriba, 1);
        
        accionAlSoltar?.Invoke();
    }

    //Funciones para botones del menú principal:

    public void IrAlMenuDeSeleccion()
    {
        CambiarAEscenaCorrespondiente();
    }

    public void IrAOpciones()
    {
        CambiarAEscenaCorrespondiente();
    }

    public void IrAlTutorial() 
    {
        CambiarAEscenaCorrespondiente();
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }

    //Funcion prueba (desechable):

    public void IrAlNivel()
    {
        CambiarAEscenaCorrespondiente();
    }

    //Funcion para boton dentro de la partida:

    public void PausarJuego()
    {
        if(JuegoManager.Instance.escenaActual != "Nivel") return;
        
        var sistemaDePartidas = GameObject.Find("ControladorPartida");
        var sistemaDePartidasFuncional = sistemaDePartidas.GetComponent<SistemaPartidas>();
        
        sistemaDePartidasFuncional.PausarJuego();
    }

    //Funciones para botones del menu de pausa:

    public void ReanudarJuego()
    {
        if(JuegoManager.Instance.escenaActual != "Nivel") return;
        
        var sistemaDePartidas = GameObject.Find("ControladorPartida");
        var sistemaDePartidasFuncional = sistemaDePartidas.GetComponent<SistemaPartidas>();
        
        sistemaDePartidasFuncional.ReanudarJuego();
    }

    public void SalirAlMenuPrincipal()
    {

        //En caso de que el botón y las escena sean las respectivas, detiene también la música y los sonidos del juego.

        if(nombreBoton == "BotonSalirAlMenu" && (JuegoManager.Instance.escenaActual == "Nivel" || JuegoManager.Instance.escenaActual == "MenuResultados"))
        {
            AudioManager.Instance.DetenerMusica();
            AudioManager.Instance.DetenerSonido();
        }

        CambiarAEscenaCorrespondiente();
    }

    //Funciones para botones del menú de opciones:

    public void CambiarGraficos()
    {
        //Función que alterna los gráficos del nivel, activándolos o 
        //desactivándolos desde el sistema de partidas en base a su 
        //estado de verdad en el JuegoManager.
        
        JuegoManager.Instance.AlternarGraficosDeFondo();
        CambiarTextoDeBoton();
    }

    //Función para la responsividad del texto del botón de los gráficos:

    public void CambiarTextoDeBotonSiCorresponde()
    {
        //Funcionalidad extra para el correcto seteo inicial del texto 
        //del botón de los gráficos:

        if(nombreBoton == "BotonAlternarGraficos")
        {
            CambiarTextoDeBoton();
        }
    }

    public void CambiarTextoDeBoton()
    {

        //Obtengo su componente de texto:
        var componenteDeTexto = GameObject.Find(nombreBoton).GetComponentInChildren<TextMeshProUGUI>();

        //Y, en base a condicional incluido en el manager general del juego, 
        //éste va a modificar su texto al mensaje correspondiente:

        if(JuegoManager.Instance.fondoActivado)
        {
            componenteDeTexto.text = "Activado";
            
        } else
        {
            componenteDeTexto.text = "Desactivado";
        }
    }

}
