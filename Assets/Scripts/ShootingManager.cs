using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{

    [Header("Disparo y encargado de otorgar el spawn de disparo:")]
    public GameObject prefabVendasPlayer;
    public JugadorManager jugador;

    [Header("Lógica de timers:")]
    public float cooldownDisparo; 
    public float tiempoActual;
    public bool seActivoElCooldown;

    void Awake()
    {
        jugador = GetComponent<JugadorManager>();
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

    public void DispararSi_(bool InputDisparoPresionado)
    {
        //En caso de que el jugador no sea una momia, no hará nada a pesar de tener el manager/componente "activo":
        if(!jugador.esMomia) return;

        if(InputDisparoPresionado)
        {
            Disparar();
        }
    }


    private void Disparar()
    {
        if(prefabVendasPlayer == null) return;

        IniciarTimerDeCooldownDeDisparo();

    }

    private void IniciarTimerDeCooldownDeDisparo()
    {
        tiempoActual = cooldownDisparo;
        
        seActivoElCooldown = true;
    }

    private void ActualizarTimerDeCooldownDeDisparo()
    {
        if(!seActivoElCooldown) return;

        tiempoActual -= Time.deltaTime;

        if(haConcluidoElCooldownDeDisparo())
        {
            var vendaActual = Instantiate(prefabVendasPlayer, jugador.shootPoint.transform.position, jugador.shootPoint.transform.rotation);
            var vendaActualFuncional = vendaActual.GetComponent<Venda>();
            vendaActualFuncional.RecibirDireccionDeDisparoEnBaseA_(jugador.shootPoint);
        }
    }

}
