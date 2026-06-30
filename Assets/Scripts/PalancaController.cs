using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalancaController : MonoBehaviour
{
    [Header("Booleanos:")]
    [SerializeField] private bool activeUnaTrampa = false;
    [SerializeField] private bool hayUnJugadorCerca = false;
    [SerializeField]private bool seInicioElTimer = false;

    [Header("Potenciales opciones de trampas:")]
    [SerializeField] private List<GameObject> listaTrampasPotenciales;

    [Header("Trampas disponibles:")]
    [SerializeField] private List<GameObject> listraTrampasDisponiblesAElegir; //a chequear

    [Header("Todas las trampas del nivel:")]
    public GameObject potencialTrampaActivable; //Vincular c/u desde el inspector.
    public GameObject potencialTrampaActivable2;
    public GameObject potencialTrampaActivable3;
    public GameObject potencialTrampaActivable4;
    
    [Header("Trampa actual y su respectiva posición de spawn:")]
    [SerializeField] private GameObject trampaActual;
    [SerializeField] private Vector2 posicionSpawnTrampa;
    
    [Header("Variables para la lógica de cooldown de la trampa:")]
    private float tiempoActual;
    private float tiempoCooldown = 5.0f;

    [Header("Prefabs con posiciones tentativas de trampas (dependiendo de que tipo sea):")]
    [SerializeField] GameObject posicionTentativaParaRoca1; //a chequear, puede ser que, dependiendo la posición en lista, sea su posición a elegir.
    [SerializeField] GameObject posicionTentativaParaRoca2;
    [SerializeField] GameObject posicionTentativaParaPinchos1;
    [SerializeField] GameObject posicionTentativaParaPinchos2;

    [SerializeField] GameObject posicionesDisponiblesAElegir;

    [SerializeField] SistemaPartidas sistemaPartidaActual;

    // Start is called before the first frame update
    void Start()
    {
        sistemaPartidaActual = GameObject.Find("ControladorParidas").GetComponent<SistemaPartidas>();
        //Elijo y spawneo correctamente la trampa:
        PresetearListasDeTrampas();
        SeleccionarTrampaInicialAleatoriamente();
    }

    // Update is called once per frame
    void Update()
    {
        actualizarTimerCooldownInterruptor();
    }

    //Booleanos:
    
    bool seConcretoElTiempoDeCooldown()
    {
        return tiempoActual <= 0;
    }

    //Funciones para el seteo y preconfiguración de la trampa que activará el interruptor:

    private void PresetearListasDeTrampas()
    {
        listaTrampasPotenciales = new List<GameObject> {potencialTrampaActivable, potencialTrampaActivable2, potencialTrampaActivable3, potencialTrampaActivable4};
        listraTrampasDisponiblesAElegir = sistemaPartidaActual.listaTrampasSinElegir;
    }

    private void SeleccionarTrampaInicialAleatoriamente()
    {

        var indexAleatorio = Random.Range(0, listaTrampasPotenciales.Count);

        trampaActual = listaTrampasPotenciales[indexAleatorio];

        print($"la trampa elegida es la siguiente: {trampaActual.name}");

        /*
        if(trampaActual.name == "TrampaRoca")
        {
            posicionSpawnTrampa = new Vector2(3.12f, 4.22f); //Cambiarlo por la
        } else
        {
            posicionSpawnTrampa = new Vector2(0.1748f, -4.68f);
        }
        */

        if(trampaActual.name == "TrampaRoca")
        {
            posicionSpawnTrampa = new Vector2(3.12f, 4.22f); //Cambiarlo por la
        } else
        {
            posicionSpawnTrampa = new Vector2(0.1748f, -4.68f);
        }
    }

    //Triggers para la comprensión de colisión del jugador con la palanca:

    private void OnTriggerEnter2D(Collider2D other) {
        if(JuegoManager.Instance.elTagDelObjeto_SeEncuentraEnLaListaDeTagsDeJugadores(other.gameObject))
        {
            hayUnJugadorCerca = true;
            print("Hay un jugador que puede activar la trampa");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(JuegoManager.Instance.elTagDelObjeto_SeEncuentraEnLaListaDeTagsDeJugadores(other.gameObject))
        {
            hayUnJugadorCerca = false;
            print("El jugador ya no se encuentra cerca del interruptor.");
        }
    }

    // Este método público será llamado directamente por el jugador cuando presione la tecla
    public void IntentarActivarInterruptor()
    {
        if (activeUnaTrampa || !hayUnJugadorCerca) return;

        print("Acabo de activar la trampa de forma segura");
        
        activeUnaTrampa = true;

        Instantiate(trampaActual, posicionSpawnTrampa, Quaternion.identity);

        iniciarTimerCooldownInterruptor();
    }

    //Funciones para lógica de cooldown:

    void iniciarTimerCooldownInterruptor()
    {
        if(!activeUnaTrampa) return;

        tiempoActual = tiempoCooldown;

        seInicioElTimer = true;
    }

    void actualizarTimerCooldownInterruptor()
    {
        if(!seInicioElTimer) return; //si no se inició el timer nunca, no hace nada.

        tiempoActual -= Time.deltaTime;

        if(seConcretoElTiempoDeCooldown())
        {
            activeUnaTrampa = false;
            //print("Ahora puedo activar de nuevo la trampa");
        }
    }

}
