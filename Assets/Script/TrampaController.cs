using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaActivable : MonoBehaviour
{

    public Rigidbody2D rbTrampa;

    void Awake()
    {
        rbTrampa = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Inicializar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void Inicializar()
    {
        //Freezeo su posición y su rotación:
        rbTrampa.constraints = RigidbodyConstraints2D.FreezeRotation;
        rbTrampa.constraints = RigidbodyConstraints2D.FreezePosition;

        //Y hago que su gravedad sea 0:
        rbTrampa.gravityScale = 0;
    }

    public void ActivarTrampa()
    {
        rbTrampa.constraints = RigidbodyConstraints2D.None;
        rbTrampa.gravityScale = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Jugador") || collision.gameObject.CompareTag("Plataformas"))
        {
            DestruirLuegoDeUnTiempo();
        }

    }
    private void DestruirLuegoDeUnTiempo()
    {
        Destroy(gameObject, 5f);
    }
}
