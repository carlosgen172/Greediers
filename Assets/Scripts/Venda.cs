using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venda : MonoBehaviour
{
    //valores de la venda:
    [Header("Valores generales del disparo de la momia:")]
    public float tiempoVida = 3.0f;
    public float velocidadDisparo;
    private Rigidbody2D rbBala;
    private float impulsoDisparo;
    private GameObject disparador;
    private int tesoroAQuitar;
    private List<string> listaTagsObjetosColisionables = new List<string> { "Plataforma", "Roca_1", "Roca_2", "Pinchos_1", "Pinchos_2", "Limite", "jugador" };

    void Awake()
    {
        rbBala = GetComponent<Rigidbody2D>();

        tesoroAQuitar = 5;
        velocidadDisparo = 2.5f;
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
        if (this == null) return;

        Destroy(gameObject, tiempoVida);
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
        if (collision == null) return;


        if (elObjetoAlQueColisioneEsUnJugador(collision.gameObject))
        {
            Debug.Log("ha colisionado con el jugador: " + collision.gameObject.name);
            var puntajeJugador = collision.gameObject.GetComponent<PuntajeJugadorController>();
            puntajeJugador.PerderTesoro(tesoroAQuitar);
            Debug.Log("el puntaje es: " + puntajeJugador.puntaje);
        }

        Destroy(gameObject);
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