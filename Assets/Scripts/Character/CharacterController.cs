using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******Estados del personaje******/
public enum CharacterControllerState
{
    IDDLE,
    WALK,
    JUMP,
    DOUBLEJUMP,
    WALLJUMP,
    AFTERJUMP,
    FALL,
    WALL
}
/**********************************/

public class CharacterController : MonoBehaviour
{   
    /***********************Parámetros de las mecánicas del personaje*************************************/
    private float fuerzaSalto          = 50;     // x veces la masa del personaje
    private float fuerzaImpulso        = 25000;  // Fuerza en Newtons
    private float fuerzaDesplazamiento = 1000;   // Fuerza en Newtons

    private float sensibilidadCaida    = 0.5f;   // Alcanzada esta velocidad descendente se considera que el personaje está cayendo
    private float sensibilidadRotacion = 0.3f;   // Alcanzada esta velocidad circular se considera que el personaje está rotando

    private float longitudRaycast      = 0.6f;   // Longitud de los rayos laterales para detectar muros
    /*****************************************************************************************************/


    /***********************Holders para las referencias a otros objetos**********************************/
    private Rigidbody2D rb2d;       // Variable para mantener la referencia al componente Rigidbody2D
    private Animator animator;      // Variable para mantener la referencia al componente Animator
    private SpriteRenderer spriteR; // Variable para mantener la referencia al componente SpriteRenderer
    private CharacterStatsManager manager;
    private InteractionEngine characterInteractionPublisher;
    /*****************************************************************************************************/

    
    /**********************Atributos internos del objeto**************************************/
    private RaycastHit2D HitL, HitR;

    private bool enElMuroL = false; // Bandera que verifica que el personaje ha tocado el muro izquierdo
    private bool hasJumped = false; // Bandera que indica que ya se ejecutó un salto
    private CharacterControllerState estado;    // Mantiene el estado actual del personaje
    private float fuerzaH;
    private float fuerzaV;
    /******************************************************************************************/

    /**********Referencias a otros objetos desde Unity*****/
    [SerializeField] private AudioSource salto_SFX;
    [SerializeField] private LayerMask rayMask;
    /******************************************************/
    
    void Start()
    {
        Debug.Log("[CharacterController] - Start");

        manager = CharacterStatsManager.getInstance();
        rb2d = GetComponent<Rigidbody2D>();       // Se obtiene la referencia al componente Rigidbody2D del personaje
        animator = GetComponent<Animator>();      // Se obtiene la referencia al componente Animator del personaje
        spriteR = GetComponent<SpriteRenderer>(); // Se obtiene la referencia al componente SpriteRenderer del personaje
        
        estado = CharacterControllerState.IDDLE;
        fuerzaH = fuerzaDesplazamiento;
        fuerzaV = 0;

        // Patrón Observer (como Subscriber)
        characterInteractionPublisher = GetComponent<InteractionEngine>();
        if (characterInteractionPublisher != null) // Se suscribe a los respectivos eventos
        {
            characterInteractionPublisher.CharacterLivesChanged += OnCharacterLivesChanged;
        }
    }
    private void OnDestroy()
    {
        if (characterInteractionPublisher != null) // Cancela la suscripción a los eventos
        {
            characterInteractionPublisher.CharacterLivesChanged -= OnCharacterLivesChanged;
        }
    }

    void Update()
    {
        if(PauseController.isPaused()){return;}
        dontLetTheCharacterRotateToMuch();

        /*******Condiciones de cambio de estado*******/        
        getHumanInput();
        verifyTouchingWall();
        verifyFalling();
        /*********************************************/
        //Debug.Log("[CharacterController]estado: " + estado);

        // Acciones de cada estado
        switch(estado){
            case CharacterControllerState.IDDLE:
                animator.SetBool("running", false);
                animator.SetBool("falling", false);
                animator.SetBool("wall", false);
                animator.SetBool("jump", false);
                animator.SetBool("doubleJump", false);
                rb2d.gravityScale = 1f;
                break;
            case CharacterControllerState.WALK:
                animator.SetBool("running", true);
                animator.SetBool("falling", false);
                rb2d.AddForce(new Vector2(fuerzaH, 0));
                break;
            case CharacterControllerState.JUMP:
                if(!hasJumped){
                    salto_SFX.Play();
                    //fuerza vertical - el desplazamiento horizontal lo da la inercia que lleve el personaje
                    rb2d.AddForce(new Vector2(0, fuerzaV));
                    hasJumped = true;
                }                
                animator.SetBool("jump", true);
                animator.SetBool("running", false);
                break;
            case CharacterControllerState.WALLJUMP:
                if(!hasJumped){
                    salto_SFX.Play();
                    //fuerza vertical y horizontal - como el personaje está en el aire es necesario imprimirle también fuerza horizontal
                    rb2d.AddForce(new Vector2(fuerzaH, fuerzaV));
                    hasJumped = true;
                }
                animator.SetBool("wall", false);
                animator.SetBool("jump", true);
                break;
            case CharacterControllerState.DOUBLEJUMP:
                if(!hasJumped){
                    //fuerza vertical y horizontal - como el personaje está en el aire es necesario imprimirle también fuerza horizontal
                    rb2d.AddForce(new Vector2(fuerzaH, fuerzaV));
                    hasJumped = true;
                }
                animator.SetBool("doubleJump", true);
                animator.SetBool("jump", false);
                break;
            case CharacterControllerState.AFTERJUMP:
                rb2d.gravityScale = 1f;
                break;
            case CharacterControllerState.FALL:
                animator.SetBool("falling", true);
                animator.SetBool("jump", false);
                animator.SetBool("doubleJump", false);
                break;
            case CharacterControllerState.WALL:
                animator.SetBool("wall", true);
                animator.SetBool("running", false);
                animator.SetBool("falling", false);
                animator.SetBool("jump", false);
                animator.SetBool("doubleJump", false);
                rb2d.gravityScale = 0.1f;
                break;
        }

    }

    private void dontLetTheCharacterRotateToMuch(){
        // Si el personaje está rotando mucho se vuelve a poner vertical para evitar
        // que se vaya a quedar acostado en el piso
        if(transform.rotation.z > sensibilidadRotacion || transform.rotation.z < -sensibilidadRotacion){
            //Debug.Log("[CharacterController]ROTATION: " + gameObject.transform.rotation.z);
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    private void verifyTouchingWall(){
        // Se dibujan los rayos solo para DEPURACIÓN
        // Debug.DrawRay(transform.position, longitudRaycast*transform.right, Color.red);
        // Debug.DrawRay(transform.position, -longitudRaycast*transform.right, Color.red);
        HitR = Physics2D.Raycast(transform.position, transform.right, longitudRaycast, rayMask);
        HitL = Physics2D.Raycast(transform.position, transform.right, -longitudRaycast, rayMask);

        // Personaje tocando un muro
        if((HitL.collider != null) && (estado != CharacterControllerState.WALLJUMP)){ // izquierdo
            //Debug.Log("[CharacterController]WALL LEFT");
            estado = CharacterControllerState.WALL;
            enElMuroL = true;
        }
        else if((HitR.collider != null) && (estado != CharacterControllerState.WALLJUMP)){ //derecho
            //Debug.Log("[CharacterController]WALL RIGHT");
            estado = CharacterControllerState.WALL;
            enElMuroL = false;
        }
        else if( (HitL.collider == null) &&
                 (HitR.collider == null) &&
                 (hasJumped) ){
            estado = CharacterControllerState.AFTERJUMP;
        }
    }

    private void verifyFalling(){
        // ** Detector de movimiento descendente **
        if( (rb2d.velocity.y < -sensibilidadCaida) &&
            (   (estado == CharacterControllerState.AFTERJUMP) ||
                (estado == CharacterControllerState.WALK) /* ||
                (estado == CharacterControllerState.IDDLE) */
            )
          ){
            estado = CharacterControllerState.FALL;
        }
    }

    private void getHumanInput(){
        // Se verifica que el personaje se deba mover a la izquierda o a la derecha
        if((Input.GetKey("right")||Input.GetKey("left")) && (estado==CharacterControllerState.IDDLE || estado == CharacterControllerState.WALK)){
            estado = CharacterControllerState.WALK;
            if(Input.GetKey("right")){
                fuerzaH = fuerzaDesplazamiento;
                spriteR.flipX = false;
                //Debug.Log("[CharacterController]RIGHT");
            }
            else{
                fuerzaH = -fuerzaDesplazamiento;
                spriteR.flipX = true;
                //Debug.Log("[CharacterController]LEFT");
            }
        }
        else if(!(Input.GetKey("right")||Input.GetKey("left")) && (estado==CharacterControllerState.IDDLE || estado == CharacterControllerState.WALK)){
            estado = CharacterControllerState.IDDLE;
        }

        // Se verifica que el personaje deba saltar
        if(Input.GetKeyDown("space") && (estado==CharacterControllerState.IDDLE || estado == CharacterControllerState.WALK)){
            //Debug.Log("[CharacterController]FLOOR JUMP");
            estado = CharacterControllerState.JUMP;
            fuerzaV = -fuerzaSalto*Physics2D.gravity[1]*rb2d.mass;
            hasJumped = false;
        }
        else if(Input.GetKeyDown("space") && (estado == CharacterControllerState.AFTERJUMP)){
            //Debug.Log("[CharacterController]DOUBLE JUMP");
            estado = CharacterControllerState.DOUBLEJUMP;
            float d_i = spriteR.flipX?-1:1;
            fuerzaH = d_i*fuerzaImpulso;
            fuerzaV = -0.5f*fuerzaSalto*Physics2D.gravity[1]*rb2d.mass;
            hasJumped = false;
        }
        else if(Input.GetKeyDown("space") && (estado == CharacterControllerState.WALL)){
            //Debug.Log("[CharacterController]WALL JUMP");
            estado = CharacterControllerState.WALLJUMP;
            float d_i = enElMuroL?1:-1;
            fuerzaH = d_i*fuerzaImpulso;
            fuerzaV = -0.5f*fuerzaSalto*Physics2D.gravity[1]*rb2d.mass;
            hasJumped = false;
        }
    }

    private void OnCharacterLivesChanged(int deltaLives) // Se ejecuta cuando se recibe la
    {                                                    // notificación del evento al cual se ha suscrito
        Debug.Log("[CharacterController]Lives Changed: " + deltaLives);
        if(deltaLives < 0){
            animator.SetTrigger("desappear");
            estado = CharacterControllerState.IDDLE;
            if(manager.getLives()>0){
                StartCoroutine(respawnCharacter());
            }
        }        
    }

    private IEnumerator respawnCharacter(){
        yield return new WaitForSeconds(1);
        estado = CharacterControllerState.IDDLE;
        animator.SetTrigger("respawn");
        transform.position = manager.getRespawnPoint();
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.CompareTag("Ground")){
            estado = CharacterControllerState.IDDLE;
            hasJumped = false;
            Debug.Log("[CharacterController]GROUND COLLISION");
        }
        else if(collision.collider.CompareTag("Obstaculo")){
            //enElPiso = true;
            Debug.Log("[CharacterController]OBSTACLE COLLISION");
        }
    }

    public void setMask(LayerMask mask){
        rayMask = mask;
    }
}
