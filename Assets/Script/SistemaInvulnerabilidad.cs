using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaInvulnerabilidad : MonoBehaviour
{
    [Header("Configuración")]
    public float duracionInvulnerabilidad = 10f;
    public float fuerzaDeEmpuje = 10f;

    public bool esInvulnerable = false;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ActivarInvulnerabilidad(Vector2 direccionDanio)
    {
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
        float tiempoFinal = Time.time + duracionInvulnerabilidad;
        while (Time.time < tiempoFinal)
        {
            sprite.enabled = !sprite.enabled; // efecto en el sprite
            yield return new WaitForSeconds(0.15f); // velocidad del parpadeo
        }

        sprite.enabled = true; // que sea visible cuando termime
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
}
