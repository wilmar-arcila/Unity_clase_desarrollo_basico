using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolinController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 17f;

    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("Player")){
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            Animator animatorPlayer = collider.GetComponent<Animator>();
            rb.velocity = (Vector2.up * jumpForce); // Se imprime la velocidad al personaje hacia arriba
            animatorPlayer.SetBool("jump", true);   // Animación para el personaje (salto)
            animator.SetTrigger("release");         // Animación para el trampolin
        }
    }
}
