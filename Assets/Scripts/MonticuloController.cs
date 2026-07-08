using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonticuloController : MonoBehaviour
{

    [Header("Variables de salud del montículo:")]
    public int saludMonticulo;
    [SerializeField] private int saludMaximaMonticulo;
    [SerializeField] private int saludMinimaMonticulo;

    [Header("Variables y componentes para lógica de elección de sprite en base a la vida:")]
    public SpriteRenderer controladorSprite;
    [SerializeField] private List<Sprite> listaSpritesPosibles = new List<Sprite>(4);
    public Sprite spriteFinal;


    void Awake()
    {
        InicializarValoresPrincipales();
    }
    
    void Start()
    {
        InicializarSpriteYSalud();
    }

    private void InicializarValoresPrincipales()
    {
        saludMinimaMonticulo = 3;
        saludMaximaMonticulo = 12;

        var sprite1 = Resources.Load<Sprite>("Monticulo1");
        var sprite2 = Resources.Load<Sprite>("Monticulo2");
        var sprite3 = Resources.Load<Sprite>("Monticulo3");
        var sprite4 = Resources.Load<Sprite>("Monticulo4");

        listaSpritesPosibles = new List<Sprite> { sprite1, sprite2, sprite3, sprite4 };

        controladorSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public void InicializarSpriteYSalud()
    {
        InicializarSaludRandom();
        SeleccionarSpriteEnBaseASalud();
    }

    private void InicializarSaludRandom()
    {
        saludMonticulo = Random.Range(saludMinimaMonticulo, saludMaximaMonticulo);
    }

    private void SeleccionarSpriteEnBaseASalud()
    {
        if (saludMonticulo < 5)
        {
            spriteFinal = listaSpritesPosibles[1];

        }
        else if (JuegoManager.Instance.elEntero_EsMayorQue_YmenorQue_(saludMonticulo, 4, 7))
        {
            spriteFinal = listaSpritesPosibles[3];

        }
        else if (JuegoManager.Instance.elEntero_EsMayorQue_YmenorQue_(saludMonticulo, 6, 10))
        {
            spriteFinal = listaSpritesPosibles[2];

        }
        else if (JuegoManager.Instance.elEntero_EsMayorQue_YmenorQue_(saludMonticulo, 9, 13))
        {
            spriteFinal = listaSpritesPosibles[0];
        }

        controladorSprite.sprite = spriteFinal;
    }

    public void DestruirMonticulo()
    {
        Destroy(gameObject);
    }
}