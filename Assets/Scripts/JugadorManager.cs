using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

//enumaeración de habilidades
//fuera de la clase, lo hice así para no tener que hacer otro script
//y porque así puede ser accesible para otras necesidades
public enum TipoHabilidad { SuperSalto, DobleVelocidad, DobleTamanio, Momia }

public class JugadorManager : MonoBehaviour
{
    //Componentes
    [Header("Subsistemas del jugador:")]
    public MovementJugador movementPlayer;
    public InputManager inputPlayer;
    public AnimationManager animacionesJugador;

    public bool habilidadActivada;
    public bool esDobleTamanio;
    public bool estaSobreElTesoro;
    private Coroutine corrutinaTesoro;
    private MonticuloController monticulo;

    [Header("Tag de jugador:")]
    public string tagJugador;

    private PalancaController palancaCercana;

    public static bool hayAlgunaMomiaEnJuego = false;

    [Header("Booleano para la lógica de la momia:")]
    public bool esMomia = false;

    [Header("Prefabs/GameObjects necesarios para lógicas complementarias")]
    public GameObject shootPoint; //Setearla como prefab

    [Header("Provisorios:")]
    public bool estaEnJuego = false; //Por si el jugador se crea antes de su seteo de controles.
    public string jugadorSeleccionado; //String que le podría llegar a pasar el sistema de seleccion y spawn de pjs al jugador.
    //Potencial forma de guardar los spritesheets posibles del pj:
    //public spriteSheets spritesheetsProvisorio_1;
    //public spriteSheet spritesheetProvisorio_2;
    //public spriteSheets spritesheetsProvisorio_3;
    //public spriteSheet spritesheetProvisorio_4;

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

    //para el cambio de sprite de la momia
    public SpriteRenderer spriteRenderer;
    public Sprite spriteMomia;
    private Sprite spriteOriginal;


    void Awake()
    {
        movementPlayer = GetComponent<MovementJugador>();
        inputPlayer = GetComponent<InputManager>();
        animacionesJugador = GetComponent<AnimationManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        tagJugador = gameObject.tag;
        sfx_salto = Resources.Load<AudioClip>("salto-cartoon");
        sfx_danio = Resources.Load<AudioClip>("Damage");
        sfx_disparo = Resources.Load<AudioClip>("DisparoVendas");
        sfx_habilidad_momia = Resources.Load<AudioClip>("PowerUpMomia");
        sfx_habilidad_simple = Resources.Load<AudioClip>("PowerUpNormal");
        sfx_minar = Resources.Load<AudioClip>("Excavar");
        playerInput = GetComponent<PlayerInput>();
        interactuarAction = playerInput.actions["Interaccion"];
    }

    public void RecibirInputInteraccion(InputAction.CallbackContext context)
    {
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

    // Start is called before the first frame update
    void Start()
    {
        sistemaPartidaActual = GameObject.Find("ControladorPartida").GetComponent<SistemaPartidas>();
    }

    // Update is called once per frame
    void Update()
    {
        //Interacción con la palanca:
        if (inputPlayer.InteractPressed && palancaCercana != null)
        {
            palancaCercana.IntentarActivarInterruptor();
        }

        //Interacción con el tesoro:


    }

    void FixedUpdate()
    {
        //funciones de movilidad se ejecutarán aquí (se hace desde el fixedUpdate ya que se usa lógica de físicas):

        /*

        movementPlayer.MoverJugadorConVelocidadLineal(inputPlayer.Movement);

        movementPlayer.GirarJugadorSiCorrespondeCon(inputPlayer.Movement);

        movementPlayer.SaltarJugadorSi(inputPlayer.JumpPressed);

        */
    }


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
            print("el monticulo fue detectado: " + monticulo.name);
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
        StartCoroutine(CorrutinaSuperSalto(5f));
    }

    private IEnumerator CorrutinaSuperSalto(float duracion)
    {
        habilidadActivada = true;
        print("duración de supersalto: " + duracion + "segundos");
        movementPlayer.AjustarSalto(true);
        yield return new WaitForSeconds(duracion);
        movementPlayer.AjustarSalto(false);
        print("super salto desactivado");
        habilidadActivada = false;
    }

    //-----------------------------------------DOBLE VELOCIDAD 
    public void ActivarDobleVelocidad()
    {
        StartCoroutine(CorrutinaDobleVelocidad(5f));
    }

    private IEnumerator CorrutinaDobleVelocidad(float duracion)
    {
        habilidadActivada = true;
        print("duración de doble velocidad: " + duracion + "segundos");
        movementPlayer.AjustarVelocidad(2.0f);
        yield return new WaitForSeconds(duracion);
        movementPlayer.AjustarVelocidad(1.0f);
        print("doble velocidad desactivado");
        habilidadActivada = false;
    }

    //-----------------------------------------DOBLE TAMANIO  
    public void ActivarDobleTamanio()
    {
        StartCoroutine(CorrutinaDobleTamanio(5f));
    }

    private IEnumerator CorrutinaDobleTamanio(float duracion)
    {
        habilidadActivada = true;
        esDobleTamanio = true;
        print("duración de doble tamanio: " + duracion + "segundos");
        movementPlayer.AjustarTamanio(2.0f);
        movementPlayer.AjustarVelocidad(0.5f); //la velocidad va a ser más lenta
        yield return new WaitForSeconds(duracion);
        movementPlayer.AjustarTamanio(1.0f);
        movementPlayer.AjustarVelocidad(1.0f); //se reanuda la velocidad original
        print("doble tamanio desactivado");
        habilidadActivada = false;
        esDobleTamanio = false;
    }

    public void ActivarHabilidad()
    {
        TipoHabilidad tipo = (TipoHabilidad)Random.Range(0, 4);
        // si hya habilidad activada, evita tomar otras habilidades
        if (habilidadActivada) return;
        // usar switrch para intercambiar entre los tipo de habilidades
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

        // para que no haya dos momias a la vez
        if (tipo == TipoHabilidad.Momia && hayAlgunaMomiaEnJuego)
        {
            // para que busque otra habilidad si hay momia en juego 
            ActivarHabilidad();
            return;
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
            inputPlayer.estaMinando = true;

            yield return new WaitForSeconds(1.5f);

            //inputPlayer.estaMinando = false;

            AudioManager.Instance.ReproducirSonido(sfx_minar);
            /* monticulo.saludMonticulo -= 1;
            jugadorPuntaje.puntaje += 1; */

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

        print("soy una momia");

        // de prueba
        spriteMomia = Resources.Load<Sprite>("MomiaPrueba");

        spriteOriginal = spriteRenderer.sprite;
        spriteRenderer.sprite = spriteMomia;

        movementPlayer.AjustarTamanio(2.0f);
        movementPlayer.AjustarVelocidad(0.5f);

        yield return new WaitForSeconds(duracionMomia);

        spriteRenderer.sprite = spriteOriginal;
        movementPlayer.AjustarTamanio(1.0f);
        movementPlayer.AjustarVelocidad(1.0f);

        esMomia = false;
        habilidadActivada = false;
        hayAlgunaMomiaEnJuego = false;

        print("ya no soy una momia");
    }

    public void ActivarHabilidadMomia()
    {
        StartCoroutine(CorrutinaMomia(5f));
    }

    public void VoltearSpritesheet()
    {
        spriteRenderer.flipX = true;
    }
}

