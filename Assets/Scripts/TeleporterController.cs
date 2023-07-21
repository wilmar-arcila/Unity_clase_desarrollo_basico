using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleporterController : MonoBehaviour
{
    [SerializeField] private AudioSource teleporter_SFX;
    [SerializeField] private int destinationEscene;

    private SceneController sceneController;
    private Animator animator;

    private void Start() {
        sceneController = transform.GetComponent<SceneController>();
        animator = transform.GetComponent<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D collider){
        Debug.Log("Teleporter");
        teleporter_SFX.Play();
        animator.SetTrigger("collected");

        // ESPERAR A QUE TERMINE LA REPRODUCCIÓN DEL SONIDO
        sceneController.changeScene(destinationEscene);
        Destroy(this);
    }
}
