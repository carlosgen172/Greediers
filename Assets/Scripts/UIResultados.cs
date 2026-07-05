using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIResultados : MonoBehaviour
{
    public TextMeshProUGUI textoResultadosFinales;
    void Start()
    {

        var listaOrdenada = JuegoManager.Instance.jugadoresQueLlegaron
                                        .OrderByDescending(j => j.puntaje)
                                        .ToList();
                                        
        textoResultadosFinales.text = "Tabla de posiciones: \n";

        //print(JuegoManager.Instance.jugadoresQueLlegaron.Count);
        foreach (var jugador in JuegoManager.Instance.jugadoresQueLlegaron)
        {
            textoResultadosFinales.text += jugador.nombreJugador + ": " + jugador.puntaje + " puntos\n";
        }

    }


}
