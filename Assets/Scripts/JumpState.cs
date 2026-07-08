using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : FSM_Base
{

    private FSM_Jugador fsm;

    public JumpState(FSM_Jugador fsm)
    {
        this.fsm = fsm;
    }
    public override void Enter()
    {
        if (fsm.animator == null) return;
        AnimationManager.Instance.ChangeAnimation("Jump", fsm.animator);
        Debug.Log("saltando");
    }

    public override void Update()
    {
        if (fsm.jugadorActual.movementPlayer.estaEnElSuelo && !fsm.jugadorActual.estaMinando && fsm.jugadorActual.inputPlayer.estaQuieto)
        {
            fsm.ChangeState(fsm.IdleState);
        }
        if(fsm.jugadorActual.movementPlayer.estaEnElSuelo && !fsm.jugadorActual.estaMinando && !fsm.jugadorActual.inputPlayer.estaQuieto) 
        {
            fsm.ChangeState(fsm.RunState);
        }
    }

    public override void Exit()
    {
        
    }
}
