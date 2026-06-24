using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class JuegoManager : MonoBehaviour
{
    public static JuegoManager Instance;

    [Header("Lista de jugadores pre-seteados (actualmente strings, cambiarlos a prefabs):")]
    [SerializeField] List<string> listaTagsDeJugadores = new List<string> {"Jugador_1", "Jugador_2", "Jugador_3", "Jugador_4"};

    [Header("Chequeo de escena actual:")]
    public string escenaActual;

    [Header("Booleanos importantes para actualización y pre-configuración de partida:")]
    public bool fondoActivado = true;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        //actualiza su nombre a la escena que corresponda:
        ActualizarNombreDeEscenaActual();
    }

    //Booleanos:

    //Función Booleana para la verificación de tag de jugador dentro de lista:
    public bool elTagDelObjeto_SeEncuentraEnLaListaDeTagsDeJugadores(GameObject unObjeto)
    {
        var tagDeObjetoABuscar = unObjeto.tag;
        
        var seEncuentraEnLaLista = listaTagsDeJugadores.Contains(tagDeObjetoABuscar);

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

}
