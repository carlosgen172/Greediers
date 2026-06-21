using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enumaeración de habilidades
//fuera de la clase, lo hice así para no tener que hacer otro script
//y porque así puede ser accesible para otras necesidades
public enum TipoHabilidad { SuperSalto, DobleVelocidad, DobleTamanio }
public class JugadorManager : MonoBehaviour
{

    //Componentes
    private MovementJugador movementPlayer;
    public InputManagerJugador inputPlayer;

    public bool habilidadActivada;

    void Awake()
    {
        movementPlayer = GetComponent<MovementJugador>();
        inputPlayer = GetComponent<InputManagerJugador>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //funciones de movilidad se ejecutarán aquí (se hace desde el fixedUpdate ya que se usa lógica de físicas):

        movementPlayer.MoverJugadorConVelocidadLineal(inputPlayer.Movement);

        movementPlayer.GirarJugadorSiCorrespondeCon(inputPlayer.Movement);

        movementPlayer.SaltarJugadorSi(inputPlayer.JumpPressed);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*         if(collision.gameObject.CompareTag("Trampa"))
                {
                    print("He perdido tesoro");
                    Morir(); //previsorio, no morira el player.
                } */
    }

    private void Morir()
    {
        Destroy(gameObject);
    }

    private void Inicializar()
    {
        //Insertar lógica de posicionamiento según personaje seleccionado.
    }

    //SISTEMA DE CORRUTINAS agregado el 17/06 - 20/06

    // -----------------------------------------SUPER SALTO 
    public void ActivarSuperSalto()
    {
        StartCoroutine(CorrutinaSuperSalto(10f));
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
        StartCoroutine(CorrutinaDobleVelocidad(10f));
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
        StartCoroutine(CorrutinaDobleTamanio(10f));
    }

    private IEnumerator CorrutinaDobleTamanio(float duracion)
    {
        habilidadActivada = true;
        print("duración de doble tamanio: " + duracion + "segundos");
        movementPlayer.AjustarTamanio(2.0f);
        movementPlayer.AjustarVelocidad(0.5f); //la velocidad va a ser más lenta
        yield return new WaitForSeconds(duracion);
        movementPlayer.AjustarTamanio(1.0f);
        movementPlayer.AjustarVelocidad(1.0f); //se reanuda la velocidad original
        print("doble tamanio desactivado");
        habilidadActivada = false;
    }

    public void ActivarHabilidad()
    {
        TipoHabilidad tipo = (TipoHabilidad)Random.Range(0, 3);
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
        }
    }
}
