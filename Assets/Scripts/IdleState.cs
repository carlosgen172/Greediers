using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : FSM_Base
{
    // Referencia al componente fsm del jugador
    private FSM_Jugador fsm;


    // Setea la variable fsm en base al componente del jugador
    public IdleState(FSM_Jugador fsm)
    {
        this.fsm = fsm;
    }

    public override void Enter()
    {
        Debug.Log("Estoy en Idle");
    }

    public override void Update()
    {
        if(fsm.jugadorActual.inputPlayer.seEstaMoviendo)
        {
            fsm.ChangeState(fsm.RunState);
        }

        if(fsm.jugadorActual.inputPlayer.InteractPressed)
        {
            fsm.ChangeState(fsm.MineState);
        }

        if(fsm.jugadorActual.inputPlayer.JumpPressed)
        {
            fsm.ChangeState(fsm.JumpState);
        }

    }

    public override void Exit()
    {

    }
}
