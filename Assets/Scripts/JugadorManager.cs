using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

//enumaeración de habilidades
//fuera de la clase, lo hice así para no tener que hacer otro script
//y porque así puede ser accesible para otras necesidades
public enum TipoHabilidad { Momia, SuperSalto, DobleVelocidad, DobleTamanio}

public class JugadorManager : MonoBehaviour
{

    //Componentes
    [Header("Subsistemas del jugador:")]
    public MovementJugador movementPlayer;
    public InputManager inputPlayer;
    public AnimationManager animacionesJugador;
    FSM_Jugador fsm;

    [Header("Booleanos para logicas del jugador:")]
    public bool habilidadActivada;
    public bool esDobleTamanio;
    public bool estaSobreElTesoro;
    public static bool hayAlgunaMomiaEnJuego = false;
    public bool esMomia = false;
    public bool estaMinando;

    // Componentes para la recoleccion de tesoros y activacion de trampas
    private Coroutine corrutinaTesoro;
    private MonticuloController monticulo;
    private PalancaController palancaCercana;

    [Header("Tag de jugador:")]
    public string tagJugador;

    [Header("Prefabs/GameObjects necesarios para lógicas complementarias")]
    public GameObject shootPoint;

    [Header("SFX del pj:")]
    public AudioClip sfx_salto;
    public AudioClip sfx_disparo;
    public AudioClip sfx_minar;
    public AudioClip sfx_habilidad_simple;
    public AudioClip sfx_habilidad_momia;
    public AudioClip sfx_danio;

    [Header("Referencia Al Input Action:")]
    private PlayerInput playerInput;
    private InputAction interactuarAction;
    [SerializeField] SistemaPartidas sistemaPartidaActual;

    //Para el cambio de sprite de la momia
    public SpriteRenderer spriteRenderer;
    private Color colorOriginal;
    public Sprite spriteMomia;
    private Sprite spriteOriginal;
    public float duracionMomia;
    private SistemaInvulnerabilidad sistemaInvulnerabilidad;

    // 😊🐀

    [SerializeField] private float duracionHablidadVelocidad;
    [SerializeField] private float duracionHablidadAgrandamiento;
    [SerializeField] private float duracionHablidadSuperSalto;


    void Awake()
    {
        movementPlayer = GetComponent<MovementJugador>();
        inputPlayer = GetComponent<InputManager>();
        animacionesJugador = GetComponent<AnimationManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        fsm = GetComponent<FSM_Jugador>();

        tagJugador = gameObject.tag;
        sfx_salto = Resources.Load<AudioClip>("salto-cartoon");
        sfx_danio = Resources.Load<AudioClip>("Damage");
        sfx_disparo = Resources.Load<AudioClip>("DisparoVendas");
        sfx_habilidad_momia = Resources.Load<AudioClip>("PowerUpMomia");
        sfx_habilidad_simple = Resources.Load<AudioClip>("PowerUpNormal");
        sfx_minar = Resources.Load<AudioClip>("Excavar");
        playerInput = GetComponent<PlayerInput>();
        interactuarAction = playerInput.actions["Interaccion"];
        sistemaInvulnerabilidad = GetComponent<SistemaInvulnerabilidad>();
    }

    void Start()
    {
        sistemaPartidaActual = GameObject.Find("ControladorPartida").GetComponent<SistemaPartidas>();
        duracionMomia = 10f;
        colorOriginal = spriteRenderer.color;
        duracionHablidadAgrandamiento = 10f;
        duracionHablidadSuperSalto = 5f;
        duracionHablidadVelocidad = 5f;

        estaMinando = false;
    }

    public void RecibirInputInteraccion(InputAction.CallbackContext context)
    {
        if (esMomia) return;
        if (context.started)
        {
            if (estaSobreElTesoro && corrutinaTesoro == null)
            {
                corrutinaTesoro = StartCoroutine(CorrutinaObtenerTesoro(monticulo));
            }
        }
        else if (context.canceled)
        {
            DetenerRecoleccion();
        }
    }

    public void ActivarPalancaATravesDeInput(InputAction.CallbackContext context)
    {
        if (esMomia) return;
        if (palancaCercana != null && context.started)
        {
            palancaCercana.IntentarActivarInterruptor();
        }
    }

    // Start is called before the first frame update



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Palanca"))
        {
            palancaCercana = collision.gameObject.GetComponent<PalancaController>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Palanca"))
        {
            palancaCercana = null;
        }

    }


    // El jugador detecta el interruptor de manera local
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Palanca")) // Asegúrate de que el interruptor tenga este Tag
        {
            palancaCercana = other.GetComponent<PalancaController>();
        }
        // el jugador derecta el montículo 
        if (other.CompareTag("Monticulo"))
        {
            monticulo = other.GetComponent<MonticuloController>();
            estaSobreElTesoro = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Palanca"))
        {
            palancaCercana = null;
        }

        // el jugador derecta el montículo 
        if (other.CompareTag("Monticulo"))
        {
            monticulo = null;
            estaSobreElTesoro = false;
            DetenerRecoleccion();
        }
    }



    public void Inicializar()
    {

    }

    //SISTEMA DE CORRUTINAS agregado el 20/06

    public void ActivarSuperSalto()
    {
        StartCoroutine(CorrutinaSuperSalto(duracionHablidadSuperSalto));
    }

    private IEnumerator CorrutinaSuperSalto(float duracion)
    {
        habilidadActivada = true;
        AudioManager.Instance.ReproducirSonido(sfx_habilidad_simple, 1);
        spriteRenderer.color = Color.green;
        movementPlayer.AjustarSalto(true);
        yield return new WaitForSeconds(duracion);
        movementPlayer.AjustarSalto(false);
        spriteRenderer.color = colorOriginal;
        habilidadActivada = false;
    }

    //-----------------------------------------DOBLE VELOCIDAD 
    public void ActivarDobleVelocidad()
    {
        StartCoroutine(CorrutinaDobleVelocidad(duracionHablidadVelocidad));
    }

    private IEnumerator CorrutinaDobleVelocidad(float duracion)
    {
        habilidadActivada = true;
        AudioManager.Instance.ReproducirSonido(sfx_habilidad_simple, 1);
        spriteRenderer.color = Color.blue;
        movementPlayer.AjustarVelocidad(2.0f);
        yield return new WaitForSeconds(duracion);
        movementPlayer.AjustarVelocidad(1.0f);
        spriteRenderer.color = colorOriginal;
        habilidadActivada = false;
    }

    //-----------------------------------------DOBLE TAMANIO  
    public void ActivarDobleTamanio()
    {
        StartCoroutine(CorrutinaDobleTamanio(duracionHablidadAgrandamiento));
    }

    private IEnumerator CorrutinaDobleTamanio(float duracion)
    {
        habilidadActivada = true;
        esDobleTamanio = true;
        AudioManager.Instance.ReproducirSonido(sfx_habilidad_simple, 1);
        spriteRenderer.color = Color.yellow;
        movementPlayer.AjustarTamanio(2.0f);
        movementPlayer.AjustarVelocidad(0.5f); //la velocidad va a ser más lenta
        yield return new WaitForSeconds(duracion);
        movementPlayer.AjustarTamanio(1.0f);
        movementPlayer.AjustarVelocidad(1.0f); //se reanuda la velocidad original
        spriteRenderer.color = colorOriginal;
        habilidadActivada = false;
        esDobleTamanio = false;
    }

    public void ActivarHabilidad()
    {
        TipoHabilidad tipo = (TipoHabilidad)Random.Range(0, 4);
        // si hay habilidad activada, evita tomar otras habilidades
        if (habilidadActivada) return;
        // usar switch para intercambiar entre los tipo de habilidades
        switch (tipo)
        {
            case TipoHabilidad.SuperSalto:
                ActivarSuperSalto();
                break;

            case TipoHabilidad.DobleVelocidad:
                ActivarDobleVelocidad();
                break;

            case TipoHabilidad.DobleTamanio:
                ActivarDobleTamanio();
                break;

            case TipoHabilidad.Momia:
                ActivarHabilidadMomia();
                break;
        }
    }

    public void DetenerRecoleccion()
    {
        if (corrutinaTesoro != null)
        {
            StopCoroutine(corrutinaTesoro);
            corrutinaTesoro = null;
        }
    }

    private IEnumerator CorrutinaObtenerTesoro(MonticuloController monticulo)
    {
        PuntajeJugadorController jugadorPuntaje = GetComponent<PuntajeJugadorController>();
        UIManager ui = FindObjectOfType<UIManager>();

        while (monticulo != null && monticulo.saludMonticulo > 0)
        {
            yield return new WaitForSeconds(1.1f);

            AudioManager.Instance.ReproducirSonido(sfx_minar, 1.5f);

            int cantidadRecolectada = esDobleTamanio ? 2 : 1;

            monticulo.saludMonticulo -= cantidadRecolectada;
            jugadorPuntaje.puntaje += cantidadRecolectada;


            if (ui != null)
            {
                ui.ActualizarPuntaje(jugadorPuntaje);
            }

            if (monticulo.saludMonticulo <= 0)
            {
                int otorgarHabilidad = Random.Range(0, 2);

                if (otorgarHabilidad == 1 && !habilidadActivada)
                {
                    ActivarHabilidad();
                }

                monticulo.DestruirMonticulo();
            }
        }
    }

    private IEnumerator CorrutinaMomia(float duracionMomia)
    {
        esMomia = true;
        habilidadActivada = true;
        hayAlgunaMomiaEnJuego = true;

        // de prueba
        spriteMomia = Resources.Load<Sprite>("MomiaPrueba");


        AudioManager.Instance.ReproducirSonido(sfx_habilidad_momia, 1);

        movementPlayer.AjustarTamanio(2.0f);
        movementPlayer.AjustarVelocidad(0.5f);

        if (movementPlayer.estoyMirandoALaIzquierda())
        {
            spriteRenderer.flipX = true;
        }

        StartCoroutine(sistemaInvulnerabilidad.CorrutinaInvulnerabilidadMomia(duracionMomia));

        yield return new WaitForSeconds(duracionMomia);


        movementPlayer.AjustarTamanio(1.0f);
        movementPlayer.AjustarVelocidad(1.0f);

        esMomia = false;
        habilidadActivada = false;
        hayAlgunaMomiaEnJuego = false;
    }

    public void ActivarHabilidadMomia()
    {
        StartCoroutine(CorrutinaMomia(duracionMomia));
    }

    public void VoltearSpritesheet()
    {
        spriteRenderer.flipX = true;
    }

    public void EstaSaltando(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            fsm.ChangeState(fsm.JumpState);
        }
    }

    public void EstaMinando(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            estaMinando = true;
            fsm.ChangeState(fsm.MineState);
        }
        else if (context.canceled)
        {
            estaMinando = false;
        }
    }
}