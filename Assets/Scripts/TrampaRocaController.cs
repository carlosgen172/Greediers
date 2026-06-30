using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaRocaController : TrampaBaseController
{

    //Inicialización de trampa (En TRAMPABASE):

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
        if(JuegoManager.Instance.elTagDelObjeto_SeEncuentraEnLaListaDeTagsDeJugadores(collision.gameObject) || collision.gameObject.CompareTag("Plataformas"))
        {
            //collision.gameObject.GetComponent<JugadorManager>().SufrirDanio(); //DESCOMENTAR AL CREAR AL JUGADOR.
            DestruirTrampa();
        }

    }
}
