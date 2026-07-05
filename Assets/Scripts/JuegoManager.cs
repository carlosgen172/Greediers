using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class JuegoManager : MonoBehaviour
{
    public static JuegoManager Instance;

    GameObject objetoSeleccionJugador;

    [Header("Lista de jugadores pre-seteados (actualmente strings, cambiarlos a prefabs):")]

    [SerializeField] List<string> listaNombresDeJugadores = new List<string> {"Jugador_1", "Jugador_2", "Jugador_3", "Jugador_4"};
    
    //Vincular a todos los objetos de tipo jugador dentro de la lista, esta sera la misma que los va a presetear en el escenario y los comparará para saber si todos lograron escapar.
    public List<GameObject> listaJugadoresTotales; 

    //Lista de todos los jugadores que lograron escapar, esta misma se espera que el exit/sistemaPartidas les pase la información de la misma, de ahí esta lista será usada para las funciones dl ssitema de victoria (mensaje final)

    public List<GameObject> listaPrincipalJugadores;

    // lista para usar en la tabla de puntuaciones
    public List<PuntajeJugadorController> jugadoresQueLlegaron = new List<PuntajeJugadorController>();

    [Header("Chequeo de escena actual:")]
    public string escenaActual;

    [Header("Booleanos importantes para actualización y pre-configuración de partida:")]
    public bool fondoActivado = true;

    [Header("Valores para lógica de sliders y comunicación para el sonido del nivel:")]
    Slider sliderSFX;
    Slider sliderMusica;
    float volumenMusica;
    float volumenSFX;

    [Header("Música del menú:")]
    [SerializeField] private AudioClip musicaDeMenu;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        } else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }

        musicaDeMenu = Resources.Load<AudioClip>("MenuPrincipal"); 


        //Preseteo de jugadores en lista general (son los prefabs):

        var jugador_1 = Resources.Load<GameObject>("Jugador_1");
        var jugador_2 = Resources.Load<GameObject>("Jugador_2");
        var jugador_3 = Resources.Load<GameObject>("Jugador_3");
        var jugador_4 = Resources.Load<GameObject>("Jugador_4");

        listaJugadoresTotales = new List<GameObject> {jugador_1, jugador_2, jugador_3, jugador_4};
    
    }

    // Start is called before the first frame update
    void Start()
    {
        

        //¿No debería consultar si es que se encuentra en la escena de 
        // "Seleccion de Jugador" antes de tratar de realizar la función de la misma?
        objetoSeleccionJugador = GameObject.Find("ControladorSeleccionJugador");

        InicializarSeleccionJugador();
    }

    bool InicializarSeleccionJugador()
    {
        if (objetoSeleccionJugador == null)
        {
            return false;
        }

        objetoSeleccionJugador.GetComponent<SeleccionJugador>().Initialization();
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        //actualiza su nombre a la escena que corresponda:
        ActualizarNombreDeEscenaActual();
        if(escenaActual == "MenuPrincipal" && !AudioManager.Instance.audioFondo.isPlaying)
        {
            AudioManager.Instance.ReproducirMusica(musicaDeMenu);
        }
    }

    //Booleanos:

    //Función Booleana para la verificación de nombre de jugador dentro de lista:
    public bool elNombreDelObjeto_SeEncuentraEnLaListaDeNombresDeJugadores(GameObject unObjeto)
    {
        var nombreDelObjetoABuscar = unObjeto.name;
        var seEncuentraEnLaLista = listaNombresDeJugadores.Contains(nombreDelObjetoABuscar);

        return seEncuentraEnLaLista;
    }

    //Se puede cambiar la función booleana de arriba por esta nueva y más flexible:
    public bool elElemento_SeEncuentraEnLaListaDeElementos_<T>(T unElemento, List<T> unaListaDeElementos)
    {
        //Si la lista es vacía, o los datos a comparar en la lista 
        //(suponiendo que la lista tiene todos los elementos con tipo de datos 
        //iguales, que sería lo lógico), retorna falso:
        if((unaListaDeElementos.Count == 0) || (unElemento.GetType() != unaListaDeElementos[0].GetType())) return false;

        return unaListaDeElementos.Contains(unElemento);

    }

    public bool elEntero_EsMayorQue_YmenorQue_(int unEnteroAVerificar, int valorMaximo, int valorMinimo)
    {
        return (unEnteroAVerificar > valorMaximo) && (unEnteroAVerificar < valorMinimo);
    }

    //Funciones generales:

    //Función para la actualización de variable de nombre de escena actual.
    public void ActualizarNombreDeEscenaActual()
    {
        escenaActual = SceneManager.GetActiveScene().name;
    }

    //Función a revisar (esta función puede incluirse dentro de PartidasManager, en vez de aquí)
    public void CambiarALaEscenaDeLosResultados()
    {
        SceneManager.LoadScene("MenuResultados");
    }

    //Función que, con ayuda del manager de botones, completa la lógica de activación/desactivación de fondo.
    public void AlternarGraficosDeFondo()
    {
        if(escenaActual != "MenuOpciones") return;

        //Si está activado, se desactivará, sino, se reactivará. 
        //Esta variable se chequea cuando se inicializa la partida.
        fondoActivado = !fondoActivado; 
    }
}
