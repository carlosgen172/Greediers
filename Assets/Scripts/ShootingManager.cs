using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingManager : MonoBehaviour
{

    [Header("Disparo y encargado de otorgar el spawn de disparo:")]
    public GameObject prefabVendasPlayer;
    public JugadorManager jugador;

    [Header("Lógica de timers:")]
    public float cooldownDisparo; 
    public float tiempoActual;
    public bool seActivoElCooldown;
    bool puedeDisparar;
    [SerializeField] private Transform shootPoint;

    void Awake()
    {
        jugador = GetComponent<JugadorManager>();

        prefabVendasPlayer = Resources.Load<GameObject>("Venda_Momia");
    }

    void Start()
    {
        cooldownDisparo = 2f;
        puedeDisparar = true;
    }

    void Update()
    {
        ActualizarTimerDeCooldownDeDisparo();
    }

    //BOOLEANOS:

    private bool haConcluidoElCooldownDeDisparo()
    {
        return tiempoActual <= 0;
    }

    public void DispararATravesDeInput(InputAction.CallbackContext context)
    {
        if(!jugador.esMomia) return;
        if (context.started)
        {
            Disparar();
        }
    }


    private void Disparar()
    {
        if(prefabVendasPlayer == null) return;
        if(!puedeDisparar) return;
        var vendaActual = Instantiate(prefabVendasPlayer, shootPoint.position, Quaternion.identity);
        var vendaActualFuncional = vendaActual.GetComponent<Venda>();
        vendaActualFuncional.RecibirDireccionDeDisparoEnBaseA_(gameObject);
        puedeDisparar = true;

        IniciarTimerDeCooldownDeDisparo();

    }

    private void IniciarTimerDeCooldownDeDisparo()
    {
        tiempoActual = cooldownDisparo;
        
        seActivoElCooldown = true;
        puedeDisparar = false;
    }

    private void ActualizarTimerDeCooldownDeDisparo()
    {
        if(!seActivoElCooldown) return;

        tiempoActual -= Time.deltaTime;

        if(haConcluidoElCooldownDeDisparo())
        {
            puedeDisparar = true;
            tiempoActual = cooldownDisparo;
        }
    }

}
