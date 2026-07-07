using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaInvulnerabilidad : MonoBehaviour
{
    [Header("Configuración")]
    public float duracionInvulnerabilidad;
    public float fuerzaDeEmpuje = 10f;

    public bool esInvulnerable = false;
    private Rigidbody2D rb;

    private JugadorManager jugadorManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        jugadorManager = GetComponent<JugadorManager>();
    }

    void Start()
    {
        duracionInvulnerabilidad = 3f;
        fuerzaDeEmpuje = 5f;
    }

    public void ActivarInvulnerabilidad(Vector2 direccionDanio)
    {
        if(jugadorManager == null) return; // No hace nada :v
        if (esInvulnerable) return;
        StartCoroutine(CorrutinaInvulnerabilidad());
        AplicarFuerzaDeEmpuje(direccionDanio);


        
    }

    private IEnumerator CorrutinaInvulnerabilidad()
    {
        esInvulnerable = true;
        print("invulnerabilidad ACTIVADA");

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        // parpadea el personaje cambiando la transparencia
        float tiempoFinal = Time.time + duracionInvulnerabilidad; //chequear su correcta funcionalidad, time.deltaTime podría ir mejor que el time que analiza todo el tiempo de partida.
        while (Time.time < tiempoFinal)
        {
            sprite.enabled = !sprite.enabled; // efecto en el sprite
            yield return new WaitForSeconds(0.15f); // velocidad del parpadeo
        }

        sprite.enabled = true; // que sea visible cuando termime
        esInvulnerable = false;
        print("invulnerabilidad DESACTIVADA");
    }

    public IEnumerator CorrutinaInvulnerabilidadMomia(float duracion)
    {
        esInvulnerable = true;
        print("invulnerabilidad de momia ACTIVADA");
        yield return new WaitForSeconds(duracion);
        esInvulnerable = false;
        print("invulnerabilidad DESACTIVADA");
    }

    private void AplicarFuerzaDeEmpuje(Vector2 direccion)
    {
        // velocidad para que el empuje sea constante
        rb.velocity = Vector2.zero;
        // fuerza en la dirección contraria del que recibe el daño
        rb.AddForce(direccion.normalized * fuerzaDeEmpuje, ForceMode2D.Impulse);
    }

    //Realizar otra función de invulnerabilidad o usar condicional para consultar si es que el pj ha sido herido o tiene el poder de la momia.
}