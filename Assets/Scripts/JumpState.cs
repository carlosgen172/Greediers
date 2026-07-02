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
        Debug.Log("Estoy en Jump");
        MovementJugador movimiento = fsm.GetComponent<MovementJugador>();
        movimiento.SaltarJugadorSi(true);
    }

    public override void Update()
    {
        MovementJugador movimiento = fsm.GetComponent<MovementJugador>();
        if (movimiento.estaEnElSuelo)
        {
            fsm.ChangeState(fsm.IdleState);
        }
    }

    public override void Exit()
    {

    }
}
