using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FSM_Jugador : MonoBehaviour
{
    private FSM_Base currentState;

    SeleccionPersonajes seleccionPersonajes;
    AnimationManager animationManager;

    List<TextMeshProUGUI> textosJugadores = new List<TextMeshProUGUI>();

    int indiceIdles;


    public IdleState IdleState { get; private set; }
    public RunState RunState { get; private set; }
    public JumpState JumpState { get; private set; }
    public MineState MineState { get; private set; }

    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }

    public JugadorManager jugadorActual { get; private set; }

    void Awake()
    {
        seleccionPersonajes = GetComponent<SeleccionPersonajes>();
        animationManager = GetComponent<AnimationManager>();

        IdleState = new IdleState(this);
        RunState = new RunState(this);
        JumpState = new JumpState(this);
        MineState = new MineState(this);

        //se ubican los componentes DIRECTAMENTE en la fsm_jugador
        // para NO tener que ponerlos en CADA estado existente
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jugadorActual = gameObject.GetComponent<JugadorManager>();

        //poner los componentes necesarios... (si es que me olvidé de alguno)
    }

    public void Initialization()
    {
        indiceIdles = 0;

        textosJugadores = seleccionPersonajes.listaTextosJugadores;
    }

    void Start()
    {
        currentState = IdleState;
        currentState.Enter();
    }


    void Update()
    {
        currentState.Update();
    }

    public void ChangeState(FSM_Base newState)
    {
        currentState.Exit();

        currentState = newState;

        currentState.Enter();
    }

    // public int VerificacionDeJugadorConTexto_(string unTexto)
    // {
    //     for (int i = 0; i < textosJugadores.Count; i++)
    //     {
    //         if (textosJugadores[i].text == unTexto)
    //         {
    //             return i;
    //         }
    //     }
    //     return -1;
    // }

    public void ElegirIdle()
    {
        if (indiceIdles < textosJugadores.Count)
        {
            ElegirIdleDepeniendoIndice_DeListaTextos(indiceIdles);
            indiceIdles++;
        }
    }

    public void ElegirIdleDepeniendoIndice_DeListaTextos(int unIndice) //INTEGRAR A ANIMATIONMANAGER Y LUEGO CONECTARLO CON SELECCIONPERSONAJE
    {
        if (textosJugadores[unIndice].text == "JP1")
        {
            animationManager.ChangeAnimation("Idle_Pablo");
        }
        else if (textosJugadores[unIndice].text == "JP2")
        {
            animationManager.ChangeAnimation("Idle_Dario");
        }
        else if (textosJugadores[unIndice].text == "JP3")
        {
            animationManager.ChangeAnimation("Idle_Mustafa");
        }
        else if (textosJugadores[unIndice].text == "JP4")
        {
            animationManager.ChangeAnimation("Idle_Miguel");
        }
    }

    public void ElegirJumpDepeniendoIndice_DeListaTextos(int unIndice)
    {
        if (textosJugadores[unIndice].text == "JP1")
        {
            animationManager.ChangeAnimation("Jump_Pablo");
        }
        else if (textosJugadores[unIndice].text == "JP2")
        {
            animationManager.ChangeAnimation("Jump_Dario");
        }
        else if (textosJugadores[unIndice].text == "JP3")
        {
            animationManager.ChangeAnimation("Jump_Mustafa");
        }
        else if (textosJugadores[unIndice].text == "JP4")
        {
            animationManager.ChangeAnimation("Jump_Miguel");
        }
    }
}
