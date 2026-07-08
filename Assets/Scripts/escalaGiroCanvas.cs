using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escalaGiroCanvas : MonoBehaviour
{
    [Header ("Valor de escala inicial:")]
    private Vector3 escalaInicial;

    void Start()
    {
        // Guardamos la escala original del Canvas al empezar
        escalaInicial = transform.localScale;
    }

    void LateUpdate()
    {
        AjustarTextoDelCanvas();
    }
    void AjustarTextoDelCanvas()
    {
        if (transform.parent.localScale.x < 0)
        {
            transform.localScale = new Vector3(-escalaInicial.x, escalaInicial.y, escalaInicial.z);
        }
        else
        {
            transform.localScale = escalaInicial;
        }
    }
}
