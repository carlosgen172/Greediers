using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;
    AudioClip cancionMomia;
    AudioClip voice1;
    void Start()
    {
        if (instance == null){
            instance = this;
        }
        audioSource = gameObject.GetComponent<AudioSource>();
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
    }

    public void ReproducirMusica(AudioClip cancionAReproducir)
    {
        if(audioSource.isPlaying && audioSource.clip == cancionAReproducir)
        {
            return;
        }
        audioSource.clip = cancionAReproducir;
        audioSource.Play();
    }

    public void ReproducirSonido(AudioClip cancionAReproducir)
    {
        audioSource.PlayOneShot(cancionAReproducir);
    }

}
