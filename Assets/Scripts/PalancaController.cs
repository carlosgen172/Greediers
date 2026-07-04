using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalancaController : MonoBehaviour
{
    [Header("Booleanos:")]
    [SerializeField] private bool activeUnaTrampa = false;
    [SerializeField] private bool hayUnJugadorCerca = false;
    [SerializeField] private bool seInicioElTimer = false;

    [Header("Trampas disponibles:")]
    [SerializeField] private List<GameObject> listaTrampasDisponiblesAElegir;

    [Header("Trampa elegida y su posición preliminar:")]
    [SerializeField] private GameObject trampaActual;
    [SerializeField] private Vector2 posicionSpawnTrampa;

    [Header("Variables para la lógica de cooldown de la trampa:")]
    private float tiempoActual;
    private float tiempoCooldown = 5.0f;

    [Header("Sistema de partidas (para lógica de seteo y elección de trampa):")]

    [SerializeField] SistemaPartidas sistemaPartidaActual;

    // Start is called before the first frame update
    void Start()
    {
        //Seteo el sistema de partidas en base al actual (en caso de que no funcione, poner esta función en el awake):
        sistemaPartidaActual = GameObject.Find("ControladorPartida").GetComponent<SistemaPartidas>();
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

    public void InicializarPalancaEnBaseASuLista() //No se llama en su start para inicializarlas correctamente una por una dentro de la partida
    {
        PresetearListaDeTrampas();
        SeleccionarTrampaInicialAleatoriamente();
    }

    private void PresetearListaDeTrampas()
    {
        listaTrampasDisponiblesAElegir = sistemaPartidaActual.listaTrampasSinElegir;
    }

    private void SeleccionarTrampaInicialAleatoriamente()
    {
        if (listaTrampasDisponiblesAElegir.Count == 0) return;

        //var indexAleatorio = Random.Range(0, listaTrampasPotenciales.Count);
        var indexAleatorio = Random.Range(0, listaTrampasDisponiblesAElegir.Count);

        //trampaActual = listaTrampasPotenciales[indexAleatorio];
        trampaActual = listaTrampasDisponiblesAElegir[indexAleatorio];

        //Cambio el valor de indice de trampa del sistema de partidas por el que se eligió aquí:
        sistemaPartidaActual.indiceTrampaElegida = indexAleatorio;

        //Debug para saber si se eligió corectamente:
        print($"la trampa elegida es la siguiente: {trampaActual.name}");

        //Y seteamos su posición a una preliminar:
        posicionSpawnTrampa = gameObject.transform.position;

    }

    //Triggers para la comprensión de colisión del jugador con la palanca:

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (JuegoManager.Instance.elNombreDelObjeto_SeEncuentraEnLaListaDeNombresDeJugadores(other.gameObject))
        {
            hayUnJugadorCerca = true;
            print("Hay un jugador que puede activar la trampa");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (JuegoManager.Instance.elNombreDelObjeto_SeEncuentraEnLaListaDeNombresDeJugadores(other.gameObject))
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
        if (!activeUnaTrampa) return;

        tiempoActual = tiempoCooldown;

        seInicioElTimer = true;
    }

    void actualizarTimerCooldownInterruptor()
    {
        if (!seInicioElTimer) return; //si no se inició el timer nunca, no hace nada.

        tiempoActual -= Time.deltaTime;

        if (seConcretoElTiempoDeCooldown())
        {
            activeUnaTrampa = false;
        }
    }

}
