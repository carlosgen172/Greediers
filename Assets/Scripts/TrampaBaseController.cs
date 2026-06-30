using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrampaBaseController : MonoBehaviour
{
    protected Rigidbody2D rbTrampa;
    protected bool haSidoElegido = false;

    void Awake()
    {
        rbTrampa = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    protected void Start()
    {
        Inicializar();
        ActivarTrampa();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void Inicializar()
    {
        rbTrampa.constraints = RigidbodyConstraints2D.FreezeRotation;

        rbTrampa.gravityScale = 0;
    }
    protected abstract void ActivarTrampa();

    protected abstract void DestruirTrampa();

}
