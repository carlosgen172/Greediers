using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaDanio : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador1") || other.CompareTag("Jugador2"))
        {
            var invulnerabilidad = other.GetComponent<SistemaInvulnerabilidad>();
            var jugadorPuntaje = other.GetComponent<JugadorPuntaje>();

            if (invulnerabilidad != null && !invulnerabilidad.esInvulnerable)

                // prueba para ver si funciona la zona de daño perdiendo lo recolectado
                if (jugadorPuntaje != null)
                {
                    if (jugadorPuntaje.puntaje == 0) return;
                    jugadorPuntaje.puntaje -= 10;

                    // actualizacion de la ui (resta de punto si recibe daño)
                    UIManager ui = FindObjectOfType<UIManager>();

                    if (ui != null)
                    {
                        ui.ActualizarPuntaje(jugadorPuntaje);
                    }

                    // dirección del empuje 
                    Vector2 direccionDeEmpuje = (other.transform.position - transform.position).normalized;
                    invulnerabilidad.ActivarInvulnerabilidad(direccionDeEmpuje);

                    print("el jugador es invulnerable");
                }
        }
    }
}
