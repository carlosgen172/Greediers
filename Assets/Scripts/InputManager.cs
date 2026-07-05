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

    /*

    //valores de keyCode a usar:
    public KeyCode teclaSalto = KeyCode.Space;
    public KeyCode teclaInteraccion = KeyCode.Q;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement = Input.GetAxisRaw("Horizontal");
        JumpPressed = Input.GetKeyDown(teclaSalto);
        InteractPressed = Input.GetKeyDown(teclaInteraccion);
    }

    */


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
