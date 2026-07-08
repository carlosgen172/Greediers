using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineState : FSM_Base
{
    private FSM_Jugador fsm;

    public MineState(FSM_Jugador fsm)
    {
        this.fsm = fsm;
    }

    public override void Enter()
    {
        if (fsm.animator == null) return;
        AnimationManager.Instance.ChangeAnimation("Picar", fsm.animator);
        Debug.Log("EL JUGADOR ESTA MINANDO");
    }

    public override void Update()
    {
        if (fsm.jugadorActual.inputPlayer.estaQuieto && !fsm.jugadorActual.estaMinando && fsm.jugadorActual.movementPlayer.estaEnElSuelo)
        {
            fsm.ChangeState(fsm.IdleState);
        }

        if (fsm.jugadorActual.inputPlayer.seEstaMoviendo && !fsm.jugadorActual.estaMinando && fsm.jugadorActual.movementPlayer.estaEnElSuelo)
        {
            fsm.ChangeState(fsm.RunState);
        }

        if(!fsm.jugadorActual.movementPlayer.estaEnElSuelo && !fsm.jugadorActual.estaMinando)
        {
            fsm.ChangeState(fsm.JumpState);
        }
    }

    public override void Exit()
    {
        
    }
}
