using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalancaController : MonoBehaviour
{
    [Header("Booleanos:")]
    [SerializeField] private bool activeUnaTrampa = false;
    [SerializeField] private bool hayUnJugadorCerca = false;
    [SerializeField]private bool seInicioElTimer = false;

    [Header("Trampas disponibles:")]
    [SerializeField] private List<GameObject> listraTrampasDisponiblesAElegir;
    
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
        sistemaPartidaActual = GameObject.Find("ControladorParidas").GetComponent<SistemaPartidas>();
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
        listraTrampasDisponiblesAElegir = sistemaPartidaActual.listaTrampasSinElegir;
    }

    private void SeleccionarTrampaInicialAleatoriamente()
    {
        if(listraTrampasDisponiblesAElegir.Count == 0) return;

        //var indexAleatorio = Random.Range(0, listaTrampasPotenciales.Count);
        var indexAleatorio = Random.Range(0, listraTrampasDisponiblesAElegir.Count);

        //trampaActual = listaTrampasPotenciales[indexAleatorio];
        trampaActual = listraTrampasDisponiblesAElegir[indexAleatorio];

        //Cambio el valor de indice de trampa del sistema de partidas por el que se elijió aquí:
        sistemaPartidaActual.indiceTrampaElegida = indexAleatorio;

        //Debug para saber si se eligió corectamente:
        print($"la trampa elegida es la siguiente: {trampaActual.name}");

        //Y seteamos su posición a una preliminar:
        posicionSpawnTrampa = gameObject.transform.position;

        /*

        if(trampaActual.name == "TrampaRoca")
        {
            posicionSpawnTrampa = new Vector2(3.12f, 4.22f); //Cambiarlo por la
        } else
        {
            posicionSpawnTrampa = new Vector2(0.1748f, -4.68f);
        }

        */

    }

    public void InicializarPalancaEnBaseA_(List<GameObject> unaListaDeTrampas)
    {
        if(unaListaDeTrampas.Count <= 0) return;

        PresetearListasDeTrampas();
        SeleccionarTrampaInicialAleatoriamente();
    }

    //Triggers para la comprensión de colisión del jugador con la palanca:

    private void OnTriggerEnter2D(Collider2D other) {
        if(JuegoManager.Instance.elNombreDelObjeto_SeEncuentraEnLaListaDeNombresDeJugadores(other.gameObject))
        {
            hayUnJugadorCerca = true;
            print("Hay un jugador que puede activar la trampa");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(JuegoManager.Instance.elNombreDelObjeto_SeEncuentraEnLaListaDeNombresDeJugadores(other.gameObject))
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
