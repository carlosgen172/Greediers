using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIResultados : MonoBehaviour
{
    [Header("Variables para la lógica de seteo de resultados")]
    public TextMeshProUGUI textoResultadosFinales;
    private List<PuntajeJugadorController> listaOrdenada = new List<PuntajeJugadorController>();
    private AudioClip jingleVictoria;
    private AudioClip jingleEmpate;

    void Start()
    {
        InicializarResultados();

        if (listaOrdenada.Count == 0)
        {
            AudioManager.Instance.ReproducirSonido(jingleEmpate, 1);
            textoResultadosFinales.text += "TODOS MANCOS";
        }
        else
        {
            foreach (var jugador in JuegoManager.Instance.jugadoresQueLlegaron)
            {
                textoResultadosFinales.text += jugador.nombreJugador + ": " + jugador.puntaje + " puntos\n";
            }
            AudioManager.Instance.ReproducirSonido(jingleVictoria, 1);
        }
    }

    private void InicializarResultados()
    {
        jingleVictoria = Resources.Load<AudioClip>("Victory");
        jingleEmpate = Resources.Load<AudioClip>("Draw");



        listaOrdenada = JuegoManager.Instance.jugadoresQueLlegaron
                                        .OrderBy(j => j.puntaje)
                                        .ToList();

        textoResultadosFinales.text = "Tabla de posiciones: \n";
    }
}
