using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerJugador : MonoBehaviour
{
    //valores actualizables:
    public float Movement { get; private set;}

    public bool JumpPressed {get; private set;}

    public bool InteractPressed {get; private set;}

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
}
