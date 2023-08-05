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
        sceneController = GetComponent<SceneController>();
        animator = GetComponent<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Player")){
            Debug.Log("Teleporter");
            teleporter_SFX.Play();
            animator.SetTrigger("collected");
            CharacterStatsManager.GetInstance().ResetRespawnPoint();
            GameManager.GetInstance().LockCharacterStats = true;

            // ESPERAR A QUE TERMINE LA REPRODUCCIÓN DEL SONIDO
            sceneController.ChangeScene(destinationEscene);
            Destroy(this);
        }
    }
}
