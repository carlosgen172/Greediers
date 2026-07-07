using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venda : MonoBehaviour
{
    //valores de la venda:
    [Header("Valores generales del disparo de la momia:")]
    public float tiempoVida = 3.0f;
    public float velocidadDisparo = 10.0f;
    //public GameObject direccionDisparo;
    private Rigidbody2D rbBala;
    private float impulsoDisparo;
    private GameObject disparador;
    private List<string> listaTagsObjetosColisionables = new List<string> {"Plataforma", "Roca", "Pinchos", "Jugador"};

    //Valor de tesoro a quitar:
    private int tesoroAQuitar;

    void Awake()
    {
        rbBala = GetComponent<Rigidbody2D>();

        tesoroAQuitar = 10;
    }
    // Start is called before the first frame update
    void Start()
    {
        DestruirLuegoDeTiempoDeterminado();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    /*

    public void RecibirDireccionDeDisparoEnBaseA_(GameObject unaDireccion)
    {
        Vector3 escala = transform.localScale;

        if(unaDireccion.transform.localScale.x == 1)
        {
            escala.x = 1;
        } else if (unaDireccion.transform.localScale.x == -1)
        {
            escala.x = -1;
        }

        transform.localScale = escala;
    }

    public void MoverConstantementeHaciaDireccionIndicada()
    {
        transform.Translate(Vector2.right * velocidadDisparo * Time.deltaTime);
    }

    */

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

    public void OnTriggerEnter2D(Collider2D collision) //probar con oncollision si falla
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
        //return unObjetoColisionado.CompareTag("Plataformas") || unObjetoColisionado.CompareTag("Pinchos") || unObjetoColisionado.CompareTag("Roca");
        return JuegoManager.Instance.elElemento_SeEncuentraEnLaListaDeElementos_(unObjetoColisionado.tag, listaTagsObjetosColisionables);
    }
}
