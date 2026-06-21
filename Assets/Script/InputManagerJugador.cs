using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerJugador : MonoBehaviour
{
    //valores actualizables:
    public float Movement { get; private set; }

    public bool JumpPressed { get; private set; }

    public bool InteractPressed { get; private set; }

    /*     //valores de keyCode a usar:
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
        } */


    // -------------------------- PROVISORIO PARA LA PRUEB A DE DOS PERSONAJES

    [Header("Configuración de teclas")]
    public KeyCode teclaIzquierda = KeyCode.A;
    public KeyCode teclaDerecha = KeyCode.D;
    public KeyCode teclaSalto = KeyCode.Space;
    public KeyCode teclaInteraccion = KeyCode.Q;

    void Update()
    {
        // Calculamos el movimiento manualmente según las teclas asignadas
        float mov = 0;
        if (Input.GetKey(teclaIzquierda)) mov -= 1;
        if (Input.GetKey(teclaDerecha)) mov += 1;
        Movement = mov;

        JumpPressed = Input.GetKeyDown(teclaSalto);
        InteractPressed = Input.GetKeyDown(teclaInteraccion);
    }
}
