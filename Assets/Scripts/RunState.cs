using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : FSM_Base
{
    private FSM_Jugador fsm;

    public RunState(FSM_Jugador fsm)
    {
        this.fsm = fsm;
    }
    public override void Enter()
    {
        Debug.Log("Estoy en Run");
    }

    public override void Update()
    {
        if(fsm.jugadorActual.inputPlayer.estaQuieto)
        {
            fsm.ChangeState(fsm.IdleState);
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
