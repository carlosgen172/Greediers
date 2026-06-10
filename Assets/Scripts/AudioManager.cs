using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource cancionMomia;
    public AudioSource voice1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            cancionMomia.PlayOneShot(cancionMomia.clip);
            cancionMomia.volume = 0.30f;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            voice1.PlayOneShot(voice1.clip);
        }
    }

}
