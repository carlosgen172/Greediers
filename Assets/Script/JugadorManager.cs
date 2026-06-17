using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorManager : MonoBehaviour
{

    //Componentes
    private MovementJugador movementPlayer;
    public InputManagerJugador inputPlayer;

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
        if(collision.gameObject.CompareTag("Trampa"))
        {
            print("He perdido tesoro");
            Morir(); //previsorio, no morira el player.
        }
    }

    private void Morir()
    {
        Destroy(gameObject);
    }

    private void Inicializar()
    {
        //Insertar lógica de posicionamiento según personaje seleccionado.
    }

}
