using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaPinchosController : TrampaBaseController
{
    //Variables del objeto:

    [Header("Posicion a mover el objeto en y:")]
    [SerializeField] private float posicionASumarEnY;

    [Header("Lógica para el tiempo de vida de la trampa:")]
    private float tiempoActualTrampa;
    [SerializeField] float tiempoVidaTrampa;
    public bool seActivoLaTrampaPorPinchos = false;

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

    //Activación:

    protected override void ActivarTrampa()
    {
        gameObject.transform.position = new Vector3(transform.position.x, this.gameObject.transform.position.y + posicionASumarEnY, this.transform.position.z);

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
            gameObject.transform.position = new Vector3(transform.position.x, this.gameObject.transform.position.y - posicionASumarEnY, this.transform.position.z);

            DestruirTrampa();
        }
    }

    //Función de trigger para la colisión del jugador con los pinchos:
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(JuegoManager.Instance.elTagDelObjeto_SeEncuentraEnLaListaDeTagsDeJugadores(collision.gameObject))
        {
            print($"Acabo de hacer daño al jugador: {collision.gameObject.tag}");
            //collision.gameObject.GetComponent<JugadorManager>().Morir(); //DESCOMENTAR AL CREAR AL JUGADOR.
        }

    }

}
