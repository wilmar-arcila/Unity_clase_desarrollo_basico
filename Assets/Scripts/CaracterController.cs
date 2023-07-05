using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaracterController : MonoBehaviour
{
    private int vidas = 3;
    
    //float nivelPiso            = -2.66f; // Este valor representa el nivel del piso para el personaje
    float nivelTecho           = 6.22f;  // Este valor representa la parte superior de la escena
    float limiteR              = 8.37f;  // Este valor representa el límite derecho de la cámara para el personaje
    float limiteL              = -8.45f; // Este valor representa el límite izquierdo de la cámara para el personaje
    float velocidad            = 4f;     // Velocidad de desplazamiento del personaje
    float fuerzaSalto          = 50;     // x veces la masa del personaje
    float fuerzaDesplazamiento = 1000;    // Fuerza en Newtons

    bool enElPiso = false;

    [SerializeField] private AudioSource salto_SFX;
    
    void Start()
    {
        // Personaje siempre inicia en la posición (-1.92, -2.34)
        gameObject.transform.position = new Vector3(-1.92f,nivelTecho,0);
        Debug.Log("INIT");
        Debug.Log("VIDAS: " + vidas);
    }

    void Update()
    {
        if(gameObject.transform.rotation.z > 0.3 || gameObject.transform.rotation.z < -0.3){
            Debug.Log("ROTATION: " + gameObject.transform.rotation.z);
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        
        if(Input.GetKey("right") && enElPiso){
            Debug.Log("RIGHT");
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(fuerzaDesplazamiento, 0));
        }
        else if(Input.GetKey("left") && gameObject.transform.position.x > limiteL && enElPiso){
            Debug.Log("LEFT");
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-fuerzaDesplazamiento, 0));
        }

        if(Input.GetKeyDown("space") && enElPiso){
            Debug.Log("UP - enElPiso: " + enElPiso);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -fuerzaSalto*Physics2D.gravity[1]*gameObject.GetComponent<Rigidbody2D>().mass));
            salto_SFX.Play();
            enElPiso = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.transform.tag == "Ground"){
            enElPiso = true;
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
