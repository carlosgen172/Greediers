using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaRocaController : TrampaBaseController
{
    [Header ("Prefabs de ubicaciones")]
    public GameObject potencialUbicacion1;
    public GameObject potencialUbicacion2;

    public bool yaColisione = false;

    public float tiempoVida = 2.0f;

    //Inicialización de trampa (En TRAMPABASE):

    //Ubicación de la trampa:
    protected override void UbicarTrampa() {
        //Seteo sus potenciales ubicaciones:
        potencialUbicacion1 = GameObject.Find("PosPotencialSpawnRoca_1");
        potencialUbicacion2 = GameObject.Find("PosPotencialSpawnRoca_2");

        //Y lo ubico en escena:
        if(gameObject.tag == "Roca_1")
        {
            gameObject.transform.position = potencialUbicacion1.transform.position;

        } else if(gameObject.tag == "Roca_2")
        {
            gameObject.transform.position = potencialUbicacion2.transform.position;
        }

    }

    //Activación de trampa:
    protected override void ActivarTrampa()
    {
        rbTrampa.constraints = RigidbodyConstraints2D.None;
        
        rbTrampa.gravityScale = 1;
    }

    //Destrucción de trampa:
    protected override void DestruirTrampa()
    {
        gameObject.SetActive(false);
    }

    //Función de collision para la colisión del jugador con la trampa;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("jugador"))
        {
            yaColisione = true;
            var invulnerabilidad = collision.gameObject.GetComponent<SistemaInvulnerabilidad>();
            var jugadorPuntaje = collision.gameObject.GetComponent<PuntajeJugadorController>();
            var jugadorController = collision.gameObject.GetComponent<JugadorManager>();

            if (invulnerabilidad != null && !invulnerabilidad.esInvulnerable)

                // prueba para ver si funciona la zona de daño perdiendo lo recolectado
                if (jugadorPuntaje != null)
                {
                    // comparativa para evitar los números negativos  
                    jugadorPuntaje.puntaje = Mathf.Max(0, jugadorPuntaje.puntaje - 5);
                    AudioManager.Instance.ReproducirSonido(jugadorController.sfx_danio, 1);

                    // actualizacion de la ui (resta de punto si recibe daño)
                    UIManager ui = FindObjectOfType<UIManager>();

                    if (ui != null)
                    {
                        ui.ActualizarPuntaje(jugadorPuntaje);
                    }

                    // dirección del empuje 
                    Vector2 direccionDeEmpuje = (collision.transform.position - transform.position).normalized;
                    invulnerabilidad.ActivarInvulnerabilidad(direccionDeEmpuje);
                }
            DestruirTrampa();
        }

        if(collision.gameObject.CompareTag("Plataforma"))
        {
            if(yaColisione) return;
            yaColisione = true;
            DestruirTrampaConTiempo();
        }
    }

    public void DestruirTrampaConTiempo()
    {
        Destroy(gameObject, tiempoVida);
    }
}