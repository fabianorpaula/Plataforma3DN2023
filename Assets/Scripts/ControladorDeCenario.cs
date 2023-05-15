using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDeCenario : MonoBehaviour
{
    public GameObject Cena1;
    public GameObject Cena2;
    public int cenarioAtiva = 1;
    public int cenarioAnterior = 1;

    public void AtivarCen(int nCena)
    {
        if (nCena != cenarioAtiva)
        {
            switch (nCena)
            {
                case 1:
                    Cena1.SetActive(true);
                    cenarioAtiva = 1;
                    //Desativar();
                    break;
                case 2:
                    Cena2.SetActive(true);
                    cenarioAtiva = 2;
                    //Desativar();
                    break;
                default:
                    Debug.Log("Deu Ruim");
                    break;

            }
        }




    }

    public void Desativar()
    {
        switch (cenarioAnterior)
        {
            case 1:
                Cena1.SetActive(false);
                cenarioAnterior = cenarioAtiva;
                break;
            case 2:
                Cena1.SetActive(false);
                cenarioAnterior = cenarioAtiva;
                break;
            default:
                Debug.Log("Deu Ruim");
                break;

        }
    }
}
