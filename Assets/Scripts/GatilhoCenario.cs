using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatilhoCenario : MonoBehaviour
{
    // Start is called before the first frame update
    public int numeroCen;
    public ControladorDeCenario ConCen;


    private void OnTriggerEnter(Collider gatilho)
    {
        if (gatilho.gameObject.tag == "Player")
        {
            ConCen.AtivarCen(numeroCen);
        }
    }
}
