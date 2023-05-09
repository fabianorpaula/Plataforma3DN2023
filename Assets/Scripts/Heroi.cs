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
    private bool estaNochao = true;

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
        
        
        
            

        ControleAtaque();
        Pegar();
        Mover();
        Pular();
    }


    void Pular()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            if(estaNochao == true)
            {
                Corpo.AddForce(Vector3.up * 250);
                estaNochao = false;
            }
            
        }
        
    }

    void Mover()
    {
        float velocidadeZ = Input.GetAxis("Vertical") * 3;
        float velocidadeX = 0;
        Vector3 velocidadeCorrigida = velocidadeX * transform.right + velocidadeZ * transform.forward;

        Corpo.velocity = new Vector3(velocidadeCorrigida.x, Corpo.velocity.y, velocidadeCorrigida.z);
     
        if(Corpo.velocity.magnitude > 1)
        {
            ControlAnim.SetBool("Andar", true);
        }
        else
        {
            ControlAnim.SetBool("Andar", false);
        }
        Girar();
    }

    void Pegar()
    {
        if (Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            ControlAnim.SetTrigger("Pegar");
        }
    }

    void Girar()
    {
        float GiroY = Input.GetAxis("Horizontal") * 100 * Time.deltaTime;
        transform.Rotate(Vector3.up * GiroY);

    }

    void ControleAtaque()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.X))
        {
            ControlAnim.SetTrigger("Ataque");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Joystick1Button5))
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
                Corpo.AddForce(Vector3.forward * -500);
                ControlAnim.SetTrigger("TomouDano");
                if (hp <= 0)
                {

                    Morrer();
                }
            }
            
        }

        if (colidiu.gameObject.tag == "Chao")
        {
            estaNochao = true;
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
