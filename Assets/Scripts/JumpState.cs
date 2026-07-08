using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : FSM_Base
{

    // Referencia a su fsm
    private FSM_Jugador fsm;

    // Setea su estado en base al fsm del jugador
    public JumpState(FSM_Jugador fsm)
    {
        this.fsm = fsm;
    }
    
    public override void Enter()
    {
        Debug.Log("Estoy en Jump");
    }

    public override void Update()
    {
        if (fsm.jugadorActual.movementPlayer.estaEnElSuelo)
        {
            fsm.ChangeState(fsm.IdleState);
        }
    }

    public override void Exit()
    {

    }
}
