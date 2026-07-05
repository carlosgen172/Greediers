using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaPinchosController : TrampaBaseController
{
    //Variables del objeto:

    //[Header("Posicion a mover el objeto en y:")]
    //[SerializeField] private float posicionASumarEnY;

    [Header("Lógica para el tiempo de vida de la trampa:")]
    private float tiempoActualTrampa;
    [SerializeField] float tiempoVidaTrampa;
    public bool seActivoLaTrampaPorPinchos = false;

    //Prefabs de ubicaciones:
    public GameObject potencialUbicacion1;
    public GameObject potencialUbicacion2;

    // Update is called once per frame
    void Update()
    {
        //Si no se activo la trampa, no se actualiza su timer.
        if (!seActivoLaTrampaPorPinchos) return;

        ActualizarTimerDeDesactivacionDeTrampa();
    }

    //BOOLEANOS

    private bool haConcluidoElTiempoDeVidaDeLaTrampa()
    {
        return tiempoActualTrampa <= 0;
    }

    //FUNCIONES GENERALES:

    //Inicialización de trampa (En TRAMPABASE):

    //Ubicación:

    protected override void UbicarTrampa() {
        //Seteo sus potenciales ubicaciones:
        potencialUbicacion1 = GameObject.Find("PosPotencialSpawnPinchos_1");
        potencialUbicacion2 = GameObject.Find("PosPotencialSpawnPinchos_2");

        //Y lo ubico en escena:
        if(gameObject.tag == "Pinchos_1")
        {
            gameObject.transform.position = potencialUbicacion1.transform.position;
        } else if(gameObject.tag == "Pinchos_2")
        {
            gameObject.transform.position = potencialUbicacion2.transform.position;
        }

    }

    //Activación:

    protected override void ActivarTrampa()
    {
        //gameObject.transform.position = new Vector3(transform.position.x, this.gameObject.transform.position.y + posicionASumarEnY, this.transform.position.z); //DESCARTADO.

        IniciarTimerDeDesactivacionDeTrampa();

    }

    //Destrucción:

    protected override void DestruirTrampa()
    {
        gameObject.SetActive(false);
    }

    //Funciones para lógica de timers:

    private void IniciarTimerDeDesactivacionDeTrampa()
    {
        tiempoVidaTrampa = 3f;
        tiempoActualTrampa = tiempoVidaTrampa;

        seActivoLaTrampaPorPinchos = true;
    }

    private void ActualizarTimerDeDesactivacionDeTrampa()
    {
        if(!seActivoLaTrampaPorPinchos) return;

        tiempoActualTrampa -= Time.deltaTime;

        if(haConcluidoElTiempoDeVidaDeLaTrampa())
        {
            //rbTrampaPinchos.AddForce(new Vector2(0, -fuerzaAparicionPinchos), ForceMode2D.Force);
            //gameObject.transform.Translate(new Vector2(0, this.gameObject.transform.position.y - fuerzaAparicionPinchos));
            //gameObject.transform.position = new Vector3(transform.position.x, this.gameObject.transform.position.y - posicionASumarEnY, this.transform.position.z); //DESCARTADO

            DestruirTrampa();
        }
    }

    //Función de trigger para la colisión del jugador con los pinchos:
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("jugador"))
        {
            print($"Acabo de hacer daño al jugador: {collision.gameObject.tag}");

            var invulnerabilidad = collision.gameObject.GetComponent<SistemaInvulnerabilidad>();
            var jugadorPuntaje = collision.gameObject.GetComponent<PuntajeJugadorController>();
            var jugadorController = collision.gameObject.GetComponent<JugadorManager>();

            if (invulnerabilidad != null && !invulnerabilidad.esInvulnerable)

                // prueba para ver si funciona la zona de daño perdiendo lo recolectado
                if (jugadorPuntaje != null)
                {
                    // comparativa para evitar los números negativos  
                    jugadorPuntaje.puntaje = Mathf.Max(0, jugadorPuntaje.puntaje - 5);
                    AudioManager.Instance.ReproducirSonido(jugadorController.sfx_danio);

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

        }

    }

}
