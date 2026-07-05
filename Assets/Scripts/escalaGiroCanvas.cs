using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escalaGiroCanvas : MonoBehaviour
{
    private Vector3 escalaInicial;

    void Start()
    {
        // Guardamos la escala original del Canvas al empezar
        escalaInicial = transform.localScale;
    }

    void LateUpdate()
    {
        // Revisamos hacia dónde está mirando el padre (el jugador)
        if (transform.parent.localScale.x < 0)
        {
            // Si el padre es negativo, hacemos el canvas negativo para contrarrestar
            transform.localScale = new Vector3(-escalaInicial.x, escalaInicial.y, escalaInicial.z);
        }
        else
        {
            // Si el padre es positivo, mantenemos el canvas normal
            transform.localScale = escalaInicial;
        }
    }
}
