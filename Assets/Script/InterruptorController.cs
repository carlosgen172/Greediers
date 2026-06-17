using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterruptorController : MonoBehaviour
{
    [SerializeField] private bool activeUnaTrampa = false;
    public GameObject trampaActivable;

    // Start is called before the first frame update
    void Start()
    {
        trampaActivable = GameObject.Find("Trampa");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   //Función de entrada del jugador en el interruptor, funciona más no realiza la acción, probarla con OnTriggerStay(Collider2D other):

    private void OnTriggerEnter2D(Collider2D other) {
        if(activeUnaTrampa) return;

        print("Estoy en la ubicación del interruptor");

        if(other.gameObject.CompareTag("Jugador"))
        {
            var jugadorFuncional = other.gameObject.GetComponent<JugadorManager>();

            if(jugadorFuncional.inputPlayer.InteractPressed)
            {
                //Activar funcionalidad de trampa.
                print("Acabo de activar la trampa");
                activeUnaTrampa = true;

                trampaActivable.GetComponent<TrampaActivable>().ActivarTrampa();
            }
            
        }
    }
}
