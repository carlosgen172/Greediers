using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrampaBaseController : MonoBehaviour
{
    protected Rigidbody2D rbTrampa;

    void Awake()
    {
        rbTrampa = GetComponent<Rigidbody2D>();
    }

    protected void Start()
    {
        Inicializar();

        UbicarTrampa();

        ActivarTrampa();
    }

    protected void Inicializar()
    {
        rbTrampa.constraints = RigidbodyConstraints2D.FreezeRotation;

        rbTrampa.gravityScale = 0;
    }

    protected abstract void UbicarTrampa();

    protected abstract void ActivarTrampa();

    protected abstract void DestruirTrampa();
}
