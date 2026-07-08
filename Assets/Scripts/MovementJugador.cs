using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementJugador : MonoBehaviour
{
    [Header("Valores modificables e iniciales referidas al personaje:")]
    public float speed = 5f;
    public float fuerzaSalto = 5f;
    private float fuerzaSuperSalto = 10f;
    private float fuerzaSaltoInicial;
    private float speedInicial;
    private Vector3 tamanioInicial;
    private Vector2 direccionDeMovimiento;
    private Vector3 escalaOriginal;

    [Header("Valores de lógica de colision con el suelo:")]
    public LayerMask capaPlataformas;
    public float longitudLineaColision;

    public bool estaEnElSuelo;

    [Header ("Otros componentes")]
    public Rigidbody2D rbPlayer;
    public InputAction moveAction;
    private JugadorManager jugadorManager;



    void Awake()
    {
        Preconfigurar();
    }

    void Start()
    {

        rbPlayer.freezeRotation = true;

        rbPlayer.drag = 0;

        longitudLineaColision = transform.localScale.x / 1.5f;

        speed = 3f;
        escalaOriginal = transform.localScale;


    }

    // Update is called once per frame
    void Update()
    {
        //Se actualizan los valores de colisión con el suelo al dibujar una línea hacia abajo del jugador, que, en caso de colisionar con una capa pasada por parámetro (vincular la capa desde el inspector), indicará que ya se encuentra en el suelo (si su valor es nulo, significa que aún sigue en el aire:
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.down, longitudLineaColision, capaPlataformas);
        estaEnElSuelo = hit2D.collider != null;
        GirarJugadorSiCorresponde();
    }

    void FixedUpdate()
    {
        rbPlayer.velocity = new Vector2(direccionDeMovimiento.x * speed, rbPlayer.velocity.y);
    }

    public bool estoyMirandoALaIzquierda()
    {
        return escalaOriginal.x == -Mathf.Abs(escalaOriginal.x);
    }

    private void Preconfigurar()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        jugadorManager = GetComponent<JugadorManager>();

        speedInicial = speed;
        tamanioInicial = transform.localScale;
        fuerzaSaltoInicial = fuerzaSalto;
    }

    public void MoverJugadorConVelocidadLineal(InputAction.CallbackContext context)
    {
        direccionDeMovimiento = context.ReadValue<Vector2>();
    }

    public void SaltarJugadorSi(InputAction.CallbackContext context)
    {
        if (context.started && estaEnElSuelo)
        {

            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, fuerzaSalto);
            AudioManager.Instance.ReproducirSonido(jugadorManager.sfx_salto);
        }
    }

    public void GirarJugadorSiCorresponde()
    {
        // Si el jugador presiona hacia la derecha (valor positivo)
        if (direccionDeMovimiento.x > 0)
        {
            GirarALaDerecha();
        }
        // Si el jugador presiona hacia la izquierda (valor negativo)
        else if (direccionDeMovimiento.x < 0)
        {
            GirarALaIzquierda();
        }
    }

    private void GirarALaIzquierda()
    {
        Vector3 escala = transform.localScale;
        escala.x = -Mathf.Abs(escala.x);
        transform.localScale = escala;
    }
    private void GirarALaDerecha()
    {
        Vector3 escala = transform.localScale;
        escala.x = Mathf.Abs(escala.x);
        transform.localScale = escala;
    }

    //Función para dibujar la línea raycast para determinar visualmente el tamaño de la misma:
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudLineaColision);
    }

    //AGREGAR UNA HABILIDAD AL TOCAR UN OBJETO agregado el 17/06

    public void AjustarSalto(bool esPotenciado)
    {
        fuerzaSalto = esPotenciado ? fuerzaSuperSalto : fuerzaSaltoInicial;
    }

    public void AjustarVelocidad(float multiplicado)
    {
        speed = speedInicial * multiplicado;
    }

    public void AjustarTamanio(float multiplicado)
    {
        transform.localScale = tamanioInicial * multiplicado;
    }
}