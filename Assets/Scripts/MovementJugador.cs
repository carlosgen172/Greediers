using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementJugador : MonoBehaviour
{
    [Header("Valores modificables referidas a la física del personaje:")]
    public float speed = 5f;
    public float fuerzaSalto = 10f;
    private float fuerzaSuperSalto = 20f;
    private float fuerzaSaltoInicial ;
    private float speedInicial;
    private Vector3 tamanioInicial;
    public Rigidbody2D rbPlayer;

    [Header("Valores de lógica de colision con el suelo:")]
    public LayerMask capaPlataformas;
    public float longitudLineaColision;

    public bool estaEnElSuelo;

    void Awake()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        rbPlayer.freezeRotation = true;

        rbPlayer.drag = 0;

        longitudLineaColision = transform.localScale.x / 1.5f;

        speed = 15f;

        fuerzaSalto = 10f;

    }

    // Update is called once per frame
    void Update()
    {
        //Se actualizan los valores de colisión con el suelo al dibujar una línea hacia abajo del jugador, que, en caso de colisionar con una capa pasada por parámetro (vincular la capa desde el inspector), indicará que ya se encuentra en el suelo (si su valor es nulo, significa que aún sigue en el aire:
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.down, longitudLineaColision, capaPlataformas);
        estaEnElSuelo = hit2D.collider != null;
    }

    private bool estoyMirandoALaDerecha()
    {
        return transform.localScale.x == 1;
    }

    private bool estoyPresionandoALaDerechaATravesDe(float unInputDeMovimiento)
    {
        return unInputDeMovimiento > 0;
    }

    private bool estoyPresionandoALaIzquierdaATravesDe(float unInputDeMovimiento)
    {
        return unInputDeMovimiento < 0;
    }

    /*

    //Función original de movimiento descartada, sin usar la velocidad del objeto, y usando la función de movePosition del objeto.

    public void MoverJugadorCon(float inputDeMovimiento)
    {
        var posicionAMover = rbPlayer.transform.position + rbPlayer.transform.right * inputDeMovimiento * speed * Time.deltaTime;
        
        rbPlayer.MovePosition(posicionAMover);
    }

    */

    public void MoverJugadorConVelocidadLineal(float inputMovimiento)
    {
        rbPlayer.velocity = new Vector2(inputMovimiento * speed, rbPlayer.velocity.y);
    }

    public void SaltarJugadorSi(bool unaCondicion)
    {
        if(unaCondicion && estaEnElSuelo)
        {
        
            rbPlayer.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);
            
            print("he presionado la tecla de salto y estoy saltando");
        
        }
    }

    public void GirarJugadorSiCorrespondeCon(float unInput)
    {
        if((estoyPresionandoALaDerechaATravesDe(unInput) && !estoyMirandoALaDerecha()) || (estoyPresionandoALaIzquierdaATravesDe(unInput) && estoyMirandoALaDerecha()))
        {
            GirarJugador();
        }
    }

    private void GirarJugador()
    {
        Vector3 escala = transform.localScale;

        escala.x *= -1;

        transform.localScale = escala;
    }

    /*

 //Funcion descartada de salto, no funcionaba correctamente con la colisionShape del mismo jugador, ya que detectaba colisiones con plataformas desde todas las direcciones, si se quiere, se puede replantear agregando un objeto hijo de colision que se encuentre en los “pies” del jugador.

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Plataformas"))
        {
            haSaltado = false;
            print("ya estoy en el suelo");
        }
    }
    */

    //Función para dibujar la línea raycast para determinar visualmente el tamaño de la misma:
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudLineaColision);
    }

    //AGREGAR UNA HABILIDAD AL TOCAR UN OBJETO agregado el 17/06

    public void AjustarSalto(bool esPotenciado)
    {
        fuerzaSalto = esPotenciado ? fuerzaSuperSalto : fuerzaSalto;
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