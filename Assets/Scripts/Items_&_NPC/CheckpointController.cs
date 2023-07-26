using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private Animator animator;
    private CharacterStatsManager manager;

    private void Start() {
        animator = GetComponent<Animator>();
        manager = CharacterStatsManager.getInstance();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("Player")){
            manager.setRespawnPoint(transform.position); // Se establece la ubicación del checkpoint
                                                         // como punto de aparición del personaje.
            animator.SetTrigger("release");              // Animación para el checkpoint.
            GetComponent<Collider2D>().enabled = false;  // Se deshabilita el trigger en el checkpoint.
        }
    }
}
