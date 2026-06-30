using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class JuegoManager : MonoBehaviour
{
    public static JuegoManager Instance;

    [Header("Lista de jugadores pre-seteados (actualmente strings, cambiarlos a prefabs):")]
    [SerializeField] List<string> listaTagsDeJugadores = new List<string> {"Jugador_1", "Jugador_2", "Jugador_3", "Jugador_4"};

    [SerializeField] List<string> listaNombresDeJugadores = new List<string> {"Jugador_1", "Jugador_2", "Jugador_3", "Jugador_4"};

    public List<GameObject> listaPrincipalJugadores = new List<GameObject> {};

    [Header("Chequeo de escena actual:")]
    public string escenaActual;

    [Header("Booleanos importantes para actualización y pre-configuración de partida:")]
    public bool fondoActivado = true;
    Slider sliderSFX;
    Slider sliderMusica;
    AudioManager audioManager;
    float volumenMusica;
    float volumenSFX;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //actualiza su nombre a la escena que corresponda:
        ActualizarNombreDeEscenaActual();
        AsignarVolumen();
        
    }

    //Booleanos:

    //Función Booleana para la verificación de tag de jugador dentro de lista:
    public bool elTagDelObjeto_SeEncuentraEnLaListaDeTagsDeJugadores(GameObject unObjeto)
    {
        var tagDeObjetoABuscar = unObjeto.tag;
        
        var seEncuentraEnLaLista = listaTagsDeJugadores.Contains(tagDeObjetoABuscar);

        return seEncuentraEnLaLista;
    }

    public bool elNombreDelObjeto_SeEncuentraEnLaListaDeNombresDeJugadores(GameObject unObjeto)
    {
        var nombreDelObjetoABuscar = unObjeto.name;
        var seEncuentraEnLaLista = listaNombresDeJugadores.Contains(nombreDelObjetoABuscar);

        return seEncuentraEnLaLista;
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

    void AsignarVolumen()
{
    if (escenaActual == "MenuOpciones")
    {
        // 1. Si entramos a la escena pero aún no tenemos la referencia de los sliders, los buscamos
        if (sliderMusica == null || sliderSFX == null)
        {
            GameObject objMusica = GameObject.Find("SliderMusica");
            GameObject objSFX = GameObject.Find("SliderSFX");

            if (objMusica != null && objSFX != null)
            {
                sliderMusica = objMusica.GetComponent<Slider>();
                sliderSFX = objSFX.GetComponent<Slider>();
            }
            else
            {
                // Si aún no existen en la escena, salimos para evitar el crash
                return; 
            }
        }

        // 2. Si ya están asignados con éxito, actualizamos el volumen
        volumenMusica = sliderMusica.value;
        volumenSFX = sliderSFX.value;
        AudioManager.Instance.AsignarVolumen(volumenMusica, volumenSFX);
    }
}

}
