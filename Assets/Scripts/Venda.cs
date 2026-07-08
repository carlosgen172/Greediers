using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venda : MonoBehaviour
{
    //valores de la venda:
    [Header("Valores generales del disparo de la momia:")]
    public float tiempoVida = 3.0f;
    public float velocidadDisparo = 10.0f;
    private Rigidbody2D rbBala;
    private float impulsoDisparo;
    private GameObject disparador;
    private int tesoroAQuitar;
    private List<string> listaTagsObjetosColisionables = new List<string> {"Plataforma", "Roca", "Pinchos", "Jugador"};

    void Awake()
    {
        rbBala = GetComponent<Rigidbody2D>();

        tesoroAQuitar = 10;
    }
    void Start()
    {
        DestruirLuegoDeTiempoDeterminado();
    }

    void FixedUpdate()
    {
        MoverConstantementeHaciaDireccionIndicada();
    }

    public void DestruirLuegoDeTiempoDeterminado()
    {
        if(this == null) return;
        
        Destroy(this.gameObject, tiempoVida);
    }

    public void RecibirDireccionDeDisparoEnBaseA_(GameObject unDisparador)
    {
        //Recibe al objeto que efectuó el disparo, lo iguala al valor privado:
        disparador = unDisparador;
        //E iguala el impulso de disparo en base a la escala local de x del disparador(que puede ser 1 o -1)
        impulsoDisparo = disparador.transform.localScale.x;
    }

    public void MoverConstantementeHaciaDireccionIndicada()
    {
        rbBala.AddForce(new Vector2(velocidadDisparo * impulsoDisparo, 0), ForceMode2D.Impulse);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == null) return;

        if (elObjetoAlQueColisioneEsUnObstaculoColisionable(collision.gameObject))
        {
            if(collision.gameObject.TryGetComponent<PuntajeJugadorController>(out PuntajeJugadorController _puntaje) && !elObjetoAlQueColisioneSoyYo(collision.gameObject))
            {
                _puntaje.PerderTesoro(tesoroAQuitar);
            }

            Destroy(gameObject);
        }
    }

    public bool elObjetoAlQueColisioneSoyYo(GameObject objetoColisionado)
    {
        return objetoColisionado.name == disparador.name;
    }

    public bool elObjetoAlQueColisioneEsUnJugador(GameObject unObjetoColisionado)
    {
        return unObjetoColisionado.CompareTag("jugador");
    }

    public bool elObjetoAlQueColisioneEsUnObstaculoColisionable(GameObject unObjetoColisionado)
    {
        return JuegoManager.Instance.elElemento_SeEncuentraEnLaListaDeElementos_(unObjetoColisionado.tag, listaTagsObjetosColisionables);
    }
}