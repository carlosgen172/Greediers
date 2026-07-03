using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource audioFondo;
    public AudioSource audioSFX;
    AudioClip cancionMomia;
    AudioClip voice1;
    public float volumenCancion;
    public float volumenSFX;

    //Falta incluir versión corregida del manager de música (y audios respectivos a cada situación)
    // Ej: el gameManager seguramente tendrá una función para la música del menú en alguna función 
    // de inicialización.

    void Awake()
    {
        

    }
    void Start()
    {
        if(Instance != null)
        {
            Destroy(this);
        } else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
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
        audioFondo.volume = volumenCancion;
        audioFondo.loop = true;
        audioFondo.Play();
    }
    
    public void DetenerMusica()
    {
        audioFondo.loop = false;
        audioFondo.volume = 100;
        audioFondo.Stop();
    }

    public void ReproducirSonido(AudioClip cancionAReproducir)
    {
        audioSFX.PlayOneShot(cancionAReproducir);
        audioSFX.volume = volumenSFX;
    }

    public void DetenerSonido()
    {
        audioSFX.Stop();
    }
    public void AsignarVolumen(float volumenDeMusica, float volumenDeSFX)
    {
        volumenCancion = volumenDeMusica;
        volumenSFX = volumenDeSFX;
    }
}
