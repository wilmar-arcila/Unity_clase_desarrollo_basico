using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{    
    private float nivelTecho           = 6.22f;  // Este valor representa la parte superior de la escena
    private float fuerzaSalto          = 50;     // x veces la masa del personaje
    private float fuerzaImpulso        = 25000;  // Fuerza en Newtons
    private float fuerzaDesplazamiento = 1000;   // Fuerza en Newtons

    private float sensibilidadCaida    = 0.5f;   // Alcanzada esta velocidad descendente se considera que el personaje está cayendo
    private float sensibilidadRotacion = 0.3f;   // Alcanzada esta velocidad circular se considera que el personaje está rotando

    private float longitudRaycast      = 0.6f;   // Longitud de los rayos laterales para detectar muros

    private Rigidbody2D rb2d;       // Variable para mantener la referencia al componente Rigidbody2D
    private Animator animator;      // Variable para mantener la referencia al componente Animator
    private SpriteRenderer spriteR; // Variable para mantener la referencia al componente SpriteRenderer

    private RaycastHit2D HitL, HitR;

    bool enElPiso  = false; // Bandera que verifica que el personaje ha tocado el piso
    bool enElMuroL = false; // Bandera que verifica que el personaje ha tocado el muro izquierdo
    bool enElMuroR = false; // Bandera que verifica que el personaje ha tocado el muro derecho
    bool hasJumped = false; // Bandera que indica que el personaje ha realizado el primer salto

    [SerializeField] private AudioSource salto_SFX;
    [SerializeField] private LayerMask rayMask;
    
    void Start()
    {
        gameObject.transform.position = new Vector3(-1.92f,nivelTecho,0);
        Debug.Log("[CharacterController] - Start");
        rb2d = GetComponent<Rigidbody2D>();       // Se obtiene la referencia al componente Rigidbody2D del personaje
        animator = GetComponent<Animator>();      // Se obtiene la referencia al componente Animator del personaje
        spriteR = GetComponent<SpriteRenderer>(); // Se obtiene la referencia al componente SpriteRenderer del personaje
        
    }

    void Update()
    {
        // Se dibujan los rayos solo para DEPURACIÓN
        Debug.DrawRay(transform.position, longitudRaycast*transform.right, Color.red);
        Debug.DrawRay(transform.position, -longitudRaycast*transform.right, Color.red);

        HitR = Physics2D.Raycast(transform.position, transform.right, longitudRaycast, rayMask);
        HitL = Physics2D.Raycast(transform.position, transform.right, -longitudRaycast, rayMask);

        if(PauseController.isPaused()){return;}
        
        // Si el personaje está rotando mucho se vuelve a poner vertical para evitar
        // que se vaya a quedar acostado en el piso
        if(transform.rotation.z > sensibilidadRotacion || transform.rotation.z < -sensibilidadRotacion){
            Debug.Log("ROTATION: " + gameObject.transform.rotation.z);
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        
        // Se verifica que el personaje se deba mover a la izquierda o a la derecha
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

        // Si NO se está moviendo a la derecha NI a la izquierda
        // se pone en falso la bandera que cambia la animación de "running"
        // Nota: El símbolo exclamación (!) es una negación lógica
        if( !(Input.GetKey("right") || Input.GetKey("left")) ){
            animator.SetBool("running", false);
        }

        // ** Detector de movimiento descendente **
        //    Sirve para cambiar la animación del personaje y como
        //    límite para realizar un segundo salto
        if(rb2d.velocity.y < -sensibilidadCaida){
            hasJumped = false;
            animator.SetBool("falling", true);
            animator.SetBool("jump", false);
            animator.SetBool("doubleJump", false);
        }

        // Implementación del salto
        if((Input.GetKeyDown("space") && enElPiso)||(Input.GetKeyDown("space") && hasJumped)){
            Debug.Log("UP - enElPiso: " + enElPiso);
            if(hasJumped){
                // Esto se ejecuta cuando YA HA SALTADO por primera vez
                animator.SetBool("doubleJump", true);
                hasJumped = false;
                float d_i = 1;
                if(rb2d.velocity.x < 0) d_i = -1; // ¿El personaje va para la derecha o la izquierda?
                //fuerza vertical y horizontal - como el personaje está en el aire es necesario imprimirle también fuerza horizontal
                rb2d.AddForce(new Vector2(d_i*fuerzaImpulso, -0.5f*fuerzaSalto*Physics2D.gravity[1]*rb2d.mass));
            }
            else{
                // Esto se ejecuta cuando es el PRIMER SALTO
                salto_SFX.Play();
                hasJumped = true;
                animator.SetBool("jump", true);
                animator.SetBool("doubleJump", false);
                //fuerza vertical - el desplazamiento horizontal lo da la inercia que lleve el personaje
                rb2d.AddForce(new Vector2(0, -fuerzaSalto*Physics2D.gravity[1]*rb2d.mass));
            }
            enElPiso = false;
        }

        // Implementación del salto del muro
        if(Input.GetKeyDown("space") && (enElMuroL || enElMuroR)){
            Debug.Log("WALL JUMP");
            animator.SetBool("jump", true);
            if(enElMuroL){
                rb2d.AddForce(new Vector2(fuerzaImpulso, -0.5f*fuerzaSalto*Physics2D.gravity[1]*rb2d.mass));
                enElMuroL = false;
            }
            else{
                rb2d.AddForce(new Vector2(-fuerzaImpulso, -0.5f*fuerzaSalto*Physics2D.gravity[1]*rb2d.mass));
                enElMuroR = false;
            }            
        }

        // Personaje tocando un muro
        if(HitL.collider != null){ // izquierdo
            Debug.Log("WALL LEFT");
            rb2d.gravityScale = 0.1f;
            animator.SetBool("wall", true);
            enElMuroL = true;
            enElPiso = false;
        }
        else if(HitR.collider != null){ //derecho
            Debug.Log("WALL RIGHT");
            rb2d.gravityScale = 0.1f;
            animator.SetBool("wall", true);
            enElMuroR = true;
            enElPiso = false;
        }

        // Personaje en el aire
        if((HitL.collider == null) && (HitR.collider == null) && !enElPiso){
            Debug.Log("AIRE");
            animator.SetBool("wall", false);
            enElMuroL = false;
            enElMuroR = false;
            rb2d.gravityScale = 1f;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.transform.tag == "Ground"){
            enElPiso = true;
            animator.SetBool("falling", false);
            Debug.Log("GROUND COLLISION");
        }
        else if(collision.transform.tag == "Obstaculo"){
            enElPiso = true;
            Debug.Log("OBSTACLE COLLISION");
        }
    }
}
