using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Jugador : MonoBehaviour
{
    private FSM_Base currentState;

    public IdleState IdleState { get; private set; }
    public RunState RunState { get; private set; }
    public JumpState JumpState { get; private set; }
    public MineState MineState { get; private set; }
    public DeathState DeathState { get; private set; }

    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }

    public JugadorManager jugadorActual { get; private set; }

    void Awake()
    {
        IdleState = new IdleState(this);
        RunState = new RunState(this);
        JumpState = new JumpState(this);
        MineState = new MineState(this);
        DeathState = new DeathState(this);

        //se ubican los componentes DIRECTAMENTE en la fsm_jugador
        // para NO tener que ponerlos en CADA estado existente
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jugadorActual = gameObject.GetComponent<JugadorManager>();
        //poner los componentes necesarios... (si es que me olvidé de alguno)
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

    
}
