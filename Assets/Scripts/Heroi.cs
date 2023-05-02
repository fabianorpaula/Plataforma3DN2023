using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Heroi : MonoBehaviour
{
    
    private Vector3 Destino;
    public GameObject MeuAtaque;
    private Animator ControlAnim;
    private GerenciadorDeObjetos Inventario;

    //Movimento
    private Rigidbody Corpo;

    private bool podePegar = false;
    public float hp = 50;
    public bool vivo = true;
    void Start()
    {
        Inventario = GameObject.FindGameObjectWithTag("Inventario").GetComponent<GerenciadorDeObjetos>();
        Destino = new Vector3(0, 0, 0);


        Corpo = GetComponent<Rigidbody>();
        ControlAnim = GetComponent<Animator>();
    }

    void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            ControlAnim.SetTrigger("Pegar");
        }
            

        ControleAtaque();
        
        Mover();
    }


    void Mover()
    {
        float velocidadeZ = Input.GetAxis("Vertical") * 3;

        Corpo.velocity = new Vector3(0, 0, velocidadeZ);
     
        if(Corpo.velocity.magnitude > 1)
        {
            ControlAnim.SetBool("Andar", true);
        }
        else
        {
            ControlAnim.SetBool("Andar", false);
        }
    }

    void ControleAtaque()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.X))
        {
            ControlAnim.SetTrigger("Ataque");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ControlAnim.SetTrigger("Disparo");
        }





    }
    public void AtivarAtk()
    {
        MeuAtaque.SetActive(true);
    }

    public void DesativarAtk()
    {
        MeuAtaque.SetActive(false);
    }

    public void AtivarPegada()
    {
        podePegar = true;
    }

    public void DesativarPegada()
    {
        podePegar = false;
        
    }


    void AtkDistancia()
    {
        RaycastHit meuAtkD;
        if (Physics.Raycast(MeuAtaque.transform.position, transform.forward, out meuAtkD, 10f))
        {
            
            if(meuAtkD.collider.gameObject.tag == "Inimigo")
            {
                meuAtkD.collider.gameObject.GetComponent<Inimigo>().TomeiDano();
            }

        }
    }


    private void OnTriggerStay(Collider colidiu)
    {
        if (colidiu.gameObject.tag == "Pegavel")
        {
            if (podePegar == true)
            {
                string nomeObj = colidiu.gameObject.GetComponent<Item>().Nome;
                Sprite imgObj = colidiu.gameObject.GetComponent<Item>().imagemObj;
                if (Inventario.ReceberItem(nomeObj, imgObj) == true)
                {
                    Destroy(colidiu.gameObject);
                }
                else
                {
                    //faz nada
                }


                podePegar = false;
            }

        }

       
    }

    private void OnTriggerEnter(Collider colidiu)
    {
        if (colidiu.gameObject.tag == "AtkInimigo")
        {
            if(vivo == true)
            {
                hp--;
                ControlAnim.SetTrigger("TomouDano");
                if (hp <= 0)
                {

                    Morrer();
                }
            }
            
        }

    }


    public void Morrer()
    {
        vivo = false;
        ControlAnim.SetBool("Morreu", true);
    }

    public void AumentarHP(float vidaPlus)
    {
        hp = hp + vidaPlus;
    }

}
