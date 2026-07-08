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

    // Otros componentes
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        InicializarCaracteristicasPrincipales();
    }

    void Update()
    {
        actualizarTimerCooldownInterruptor();
    }

    //Booleanos:

    private bool seConcretoElTiempoDeCooldown()
    {
        return tiempoActual <= 0;
    }

    private void InicializarCaracteristicasPrincipales()
    {
        //Seteo el sistema de partidas en base al actual (en caso de que no funcione, poner esta función en el awake):
        sistemaPartidaActual = GameObject.Find("ControladorPartida").GetComponent<SistemaPartidas>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        //Y seteamos su posición a una preliminar:
        posicionSpawnTrampa = gameObject.transform.position;

    }

    //Triggers para la comprensión de colisión del jugador con la palanca:

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("jugador"))
        {
            hayUnJugadorCerca = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("jugador"))
        {
            hayUnJugadorCerca = false;
        }
    }

    // Este método público será llamado directamente por el jugador cuando presione la tecla
    public void IntentarActivarInterruptor()
    {
        if (activeUnaTrampa || !hayUnJugadorCerca) return;

        activeUnaTrampa = true;

        Instantiate(trampaActual, posicionSpawnTrampa, Quaternion.identity);
        
        spriteRenderer.flipX = true;

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
            spriteRenderer.flipX = false;
        }
    }

}
