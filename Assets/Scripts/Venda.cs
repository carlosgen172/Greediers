using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venda : MonoBehaviour
{
    //valores de la venda:
    [Header("Valores generales del disparo de la momia:")]
    public float tiempoVida = 3.0f;
    public float velocidadDisparo = 10.0f;
    //public GameObject direccionDisparo;
    private Rigidbody2D rbBala;
    private float impulsoDisparo;

    void Awake()
    {
        rbBala = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        DestruirLuegoDeTiempoDeterminado();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        MoverConstantementeHaciaDireccionIndicada();
    }

    public void DestruirLuegoDeTiempoDeterminado()
    {
        Destroy(this.gameObject, tiempoVida);
    }

    /*

    public void RecibirDireccionDeDisparoEnBaseA_(GameObject unaDireccion)
    {
        Vector3 escala = transform.localScale;

        if(unaDireccion.transform.localScale.x == 1)
        {
            escala.x = 1;
        } else if (unaDireccion.transform.localScale.x == -1)
        {
            escala.x = -1;
        }

        transform.localScale = escala;
    }

    public void MoverConstantementeHaciaDireccionIndicada()
    {
        transform.Translate(Vector2.right * velocidadDisparo * Time.deltaTime);
    }

    */

    public void RecibirDireccionDeDisparoEnBaseA_(GameObject unaDireccion)
    {
        //Iguala el impulso de disparo en base a la escala local de x (que puede ser 1 o -1)
        impulsoDisparo = unaDireccion.transform.localScale.x;
    }

    public void MoverConstantementeHaciaDireccionIndicada()
    {
        rbBala.AddForce(new Vector2(0, velocidadDisparo * impulsoDisparo), ForceMode2D.Impulse);
    }
}
