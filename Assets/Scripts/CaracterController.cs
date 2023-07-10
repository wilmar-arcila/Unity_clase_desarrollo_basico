using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaracterController : MonoBehaviour
{
    private int vidas = 3;
    
    //float nivelPiso            = -2.66f; // Este valor representa el nivel del piso para el personaje
    float nivelTecho           = 6.22f;  // Este valor representa la parte superior de la escena
    float fuerzaSalto          = 50;     // x veces la masa del personaje
    float fuerzaDesplazamiento = 1000;    // Fuerza en Newtons

    private Rigidbody2D rb2d;
    private Animator animator;
    private SpriteRenderer spriteR;

    bool enElPiso = false;

    [SerializeField] private AudioSource salto_SFX;
    
    void Start()
    {
        // Personaje siempre inicia en la posición (-1.92, -2.34)
        gameObject.transform.position = new Vector3(-1.92f,nivelTecho,0);
        Debug.Log("INIT");
        Debug.Log("VIDAS: " + vidas);
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(gameObject.transform.rotation.z > 0.3 || gameObject.transform.rotation.z < -0.3){
            Debug.Log("ROTATION: " + gameObject.transform.rotation.z);
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        
        if(Input.GetKey("right") && enElPiso){
            Debug.Log("RIGHT");
            rb2d.AddForce(new Vector2(fuerzaDesplazamiento, 0));
            animator.SetBool("running", true);
            spriteR.flipX=false;
        }
        else if(Input.GetKey("left") && enElPiso){
            Debug.Log("LEFT");
            rb2d.AddForce(new Vector2(-fuerzaDesplazamiento, 0));
            animator.SetBool("running", true);
            spriteR.flipX=true;
        }

        if( !(Input.GetKey("right") || Input.GetKey("left")) ){
            animator.SetBool("running", false);
        }

        if(Input.GetKeyDown("space") && enElPiso){
            Debug.Log("UP - enElPiso: " + enElPiso);
            rb2d.AddForce(new Vector2(0, -fuerzaSalto*Physics2D.gravity[1]*rb2d.mass));
            salto_SFX.Play();
            enElPiso = false;
            animator.SetBool("jump", true);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.transform.tag == "Ground"){
            enElPiso = true;
            animator.SetBool("jump", false);
            Debug.Log("GROUND COLLISION");
        }
        else if(collision.transform.tag == "Obstaculo"){
            enElPiso = true;
            Debug.Log("OBSTACLE COLLISION");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        Debug.Log("Caída");
        vidas -= 1;
        Debug.Log("VIDAS: " + vidas);
        if(vidas <= 0){
            Debug.Log("GAME OVER");
            vidas = 3;
        }
        gameObject.transform.position = new Vector3(-1.92f,nivelTecho,0);
    }
}
