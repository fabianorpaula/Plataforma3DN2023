using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDeCameras : MonoBehaviour
{
    public GameObject Cam1;
    public GameObject Cam2;
    public int cameraAtiva = 1;
    public int cameraAnterior = 1;

    // Start is called before the first frame update
    
    void Update()
    {
        
    }

    public void AtivarCam(int nCam)
    {
        if(nCam != cameraAtiva)
        {
            switch (nCam)
            {
                case 1:
                    Cam1.SetActive(true);
                    cameraAtiva = 1;
                    Desativar();
                    break;
                case 2:
                    Cam2.SetActive(true);
                    cameraAtiva = 2;
                    Desativar();
                    break;
                default:
                    Debug.Log("Deu Ruim");
                    break;

            }
        }
        

        
       
    }

    public void Desativar()
    {
        switch (cameraAnterior)
        {
            case 1:
                Cam1.SetActive(false);
                cameraAnterior = cameraAtiva;
                break;
            case 2:
                Cam2.SetActive(false);
                cameraAnterior = cameraAtiva;
                break;
            default:
                Debug.Log("Deu Ruim");
                break;

        }
    }
}
