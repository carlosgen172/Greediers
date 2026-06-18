using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioFondo;
    public AudioSource audioSFX;
    AudioClip cancionMomia;
    AudioClip voice1;
    void Start()
    {
        if (instance == null){
            instance = this;
        }
        cancionMomia = Resources.Load<AudioClip>("CancionMomiaPrueba");
        voice1 = Resources.Load<AudioClip>("Voice1");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReproducirMusica(cancionMomia);
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ReproducirSonido(voice1);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            DetenerMusica();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DetenerSonido();
        }
    }

    public void ReproducirMusica(AudioClip cancionAReproducir)
    {
        if(audioFondo.isPlaying && audioFondo.clip == cancionAReproducir)
        {
            return;
        }
        audioFondo.clip = cancionAReproducir;
        audioFondo.loop = true;
        audioFondo.Play();
    }
    
    public void DetenerMusica()
    {
        audioFondo.loop = false;
        audioFondo.Stop();
    }

    public void ReproducirSonido(AudioClip cancionAReproducir)
    {
        audioSFX.PlayOneShot(cancionAReproducir);
    }

    public void DetenerSonido()
    {
        audioSFX.Stop();
    }

}
