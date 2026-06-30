using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaRocaController : TrampaBaseController
{

    //Prefabs de ubicaciones:
    public GameObject potencialUbicacion1;
    public GameObject potencialUbicacion2;

    //Inicialización de trampa (En TRAMPABASE):

    //Ubicación de la trampa:
    protected override void UbicarTrampa() {
        if(gameObject.name == "Roca_1")
        {
            gameObject.transform.position = potencialUbicacion1.transform.position;
        } else
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
        if(JuegoManager.Instance.elNombreDelObjeto_SeEncuentraEnLaListaDeNombresDeJugadores(collision.gameObject) || collision.gameObject.CompareTag("Plataformas"))
        {
            //collision.gameObject.GetComponent<JugadorManager>().SufrirDanio(); //DESCOMENTAR AL CREAR AL JUGADOR.
            var invulnerabilidad = collision.gameObject.GetComponent<SistemaInvulnerabilidad>();
            var jugadorPuntaje = collision.gameObject.GetComponent<PuntajeJugadorController>();

            if (invulnerabilidad != null && !invulnerabilidad.esInvulnerable)

                // prueba para ver si funciona la zona de daño perdiendo lo recolectado
                if (jugadorPuntaje != null)
                {
                    // comparativa para evitar los números negativos  
                    jugadorPuntaje.puntaje = Mathf.Max(0, jugadorPuntaje.puntaje - 10);

                    //jugadorPuntaje.puntaje -= 10;

                    // actualizacion de la ui (resta de punto si recibe daño)
                    UIManager ui = FindObjectOfType<UIManager>();

                    if (ui != null)
                    {
                        ui.ActualizarPuntaje(jugadorPuntaje);
                    }

                    // dirección del empuje 
                    Vector2 direccionDeEmpuje = (collision.transform.position - transform.position).normalized;
                    invulnerabilidad.ActivarInvulnerabilidad(direccionDeEmpuje);

                    print("el jugador es invulnerable");
                }
            
            DestruirTrampa();
        }

    }
}
