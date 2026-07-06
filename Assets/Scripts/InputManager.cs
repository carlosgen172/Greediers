using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //valores actualizables:
    public float Movement { get; private set;}

    public bool JumpPressed {get; private set;}

    public bool InteractPressed {get; private set;}

    public bool seEstaMoviendo = false;
    public bool estaQuieto = false;
    Rigidbody2D rbPlayer;
    public InputActionReference movimiento;
    public InputActionReference interaccion;


    void Awake()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        estaQuieto = rbPlayer.velocity == new Vector2(0,0);
        seEstaMoviendo = !estaQuieto;

        JumpPressed = movimiento.action.triggered;
        InteractPressed = interaccion.action.triggered;
    }

}
