using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorOpciones : MonoBehaviour
{
    Slider sliderMusica;
    Slider sliderSFX;

    void Awake()
    {
        sliderMusica = GameObject.Find("SliderMusica").GetComponent<Slider>();
        sliderSFX = GameObject.Find("SliderSFX").GetComponent<Slider>();
    }

    void Start()
    {
        sliderMusica.value = AudioManager.Instance.volumenCancion;
        sliderSFX.value = AudioManager.Instance.volumenSFX;
    }

    void Update()
    {
        AsignarVolumen();
    }

    void AsignarVolumen()
    {
        AudioManager.Instance.volumenCancion = sliderMusica.value;
        AudioManager.Instance.volumenSFX = sliderSFX.value;
    }
}
