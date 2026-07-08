using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineState : FSM_Base
{

    // Referencia a su fsm
    private FSM_Jugador fsm;

    // Seteo el fsm en base al fsm del jugador
    public MineState(FSM_Jugador fsm)
    {
        this.fsm = fsm;
    }

    public override void Enter()
    {
        Debug.Log("Estoy en Mine");
    } 

    public override void Update()
    {
        if(fsm.jugadorActual.inputPlayer.estaQuieto)
        {
            fsm.ChangeState(fsm.IdleState);
        }

        if(fsm.jugadorActual.inputPlayer.seEstaMoviendo)
        {
            fsm.ChangeState(fsm.RunState);
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
