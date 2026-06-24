using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaPartidas : MonoBehaviour
{
    [Header("Booleanos a usar:")]
    public bool yaInicio = false;
    public bool estaEnFaseDeRecoleccion = false;
    public bool estaEnFaseDeEscape = false;
    public bool yaFinalizo = false;

    [Header("Variables para lógicas de timers")]
    public float tiempoPartida = 120f;
    public float tiempoEscape = 30f;
    public float tiempoActual;

    [Header("Objetos desactivables de la escena (Lógica de funcionalidad de menú de pausa):")]

    //Fondo Desactivabe:
    [SerializeField] private GameObject fondoNivel;
    
    //Grupo de objetos del menú de pausa:
    [SerializeField] private GameObject menuPausa;
    
    //Botón desactivable:
    [SerializeField] private GameObject botonPausa;

    // Start is called before the first frame update
    void Start()
    {
        InicializarValoresPresetados();
        IniciarPartida();
    }

    // Update is called once per frame
    void Update()
    {
        //Si ya inició/ya finalizó la partida, no hara nada más este código.
        if(!yaInicio || yaFinalizo) return;

        ActualizarTimerCorrespondiente();
    }

    //Booleanos:

    public bool SeTerminoElTiempo()
    {
        return tiempoActual <= 0;
    }

    public bool laPartidaTerminoAntes()
    {
        //condicional activado cuando todos los jugadores salgan 
        //del escenario antes del tiempo acordado.
        return true; //cambiarlo por condición que corresponda.
    }

    //Funciones para la preconfiguración e incio de partida:

    public void InicializarValoresPresetados()
    {
        //Acá iría la lógica que pasaría a los jugadores seteados dentro del menu de seleccion de pjs.
        //y también se guardarían los cambios para inicializar el juego:
        
        fondoNivel = GameObject.Find("FondoNivel");

        menuPausa = GameObject.Find("MenuPausa");

        botonPausa = GameObject.Find("BotonPausa");
        
        //fondoNivel = Resources.Load<GameObject>("Fondo_1");
        
        if(JuegoManager.Instance.fondoActivado)
        {
            ActivarFondo();
        } else
        {
            DesactivarFondo();
        }

        DesactivarMenuDePausa();

        //Seteo los valores tentativos de duración de partida (2 minutos de recolección y 30 segundos de escape):
        tiempoPartida = 120f;
        
        tiempoEscape = 30f;
    }

    public void IniciarPartida()
    {
        //Seteo la escala de tiempo a 1:
        Time.timeScale = 1f;
        
        //Seteo el booleano de inicio a 1:
        yaInicio = true;
        
        //E inicio su timer de recolección:
        IniciarTimerDePartidaRecoleccion();
    }

    //Funciones para la lógica de timers:

    //Inicialización de timers:

    public void IniciarTimerDePartidaRecoleccion()
    {
        tiempoActual = tiempoPartida;
        
        estaEnFaseDeRecoleccion = true;

        print("Ha iniciado el tiempo de recolección");
    }

    public void IniciarTimerDePartidaEscape()
    {
        tiempoActual = tiempoEscape;

        estaEnFaseDeEscape = true;

        print("Ha iniciado el tiempo de escape");
    }

    //Actualización de timer y realización de acción correspondiente:

    public void ActualizarTimerCorrespondiente()
    {
        if(!yaInicio) return;

        tiempoActual -= Time.deltaTime;

        if(estaEnFaseDeRecoleccion)
        {
            ActualizarTimerDeRecoleccion();

        } else if (estaEnFaseDeEscape)
        {
            ActualizarTimerDeEscape();
        }
    }

    public void ActualizarTimerDeRecoleccion()
    {
        if(!estaEnFaseDeRecoleccion) return;

        if(SeTerminoElTiempo())
        {
            print("Se terminó el tiempo de recolección, hora de escapar!");
            estaEnFaseDeRecoleccion = false;

            IniciarTimerDePartidaEscape();
        }
    }

    public void ActualizarTimerDeEscape()
    {
        if(!estaEnFaseDeEscape) return;

        if(SeTerminoElTiempo())
        {
            print("Se acaba de terminar el tiempo de escape.");
            estaEnFaseDeEscape = false;
            FinalizarPartida();
        }
    }

    //Función para la finalización de partida y comunicación de resultados 
    //a la escena siguiente:

    public void FinalizarPartida()
    {
        Time.timeScale = 0f;

        yaFinalizo = true;
        
        //SistemaVictoria.ComunicarResultados();
        //SistemaVictoria.MostrarResultados();
    }

    //Funciones para la correcta responsividad y funcionalidad de los elementos 
    //del hud y del fondo del nivel:

    public void ActivarFondo()
    {
        fondoNivel.SetActive(true);
    }

    public void DesactivarFondo()
    {
        fondoNivel.SetActive(false);
    }

    public void PausarJuego()
    {
        if(!yaInicio || yaFinalizo) return;

        Time.timeScale = 0f;

        ActivarMenuDePausa();
    }

    public void ActivarMenuDePausa()
    {
        menuPausa.SetActive(true);

        botonPausa.SetActive(false);
    }

    public void DesactivarMenuDePausa()
    {
        menuPausa.SetActive(false);

        botonPausa.SetActive(true);
    }

    public void ReanudarJuego()
    {
        if(!yaInicio || yaFinalizo) return;

        Time.timeScale = 1f;

        DesactivarMenuDePausa();
    }

}
