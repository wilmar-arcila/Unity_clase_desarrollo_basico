using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEngine : MonoBehaviour
{
    [SerializeField] private GameObject statsRenderer;
    private StatsController statsRendererController;

    [SerializeField] private GameObject gameOverRenderer;
    private GameOverController gameoverRendererController;

    private Animator animator;
    private CharacterStatsManager manager;

    void Start()
    {
        animator = GetComponent<Animator>();
        manager = GetComponent<CharacterStatsManager>();
        statsRendererController = statsRenderer.transform.GetComponent<StatsController>();
        gameoverRendererController = gameOverRenderer.transform.GetComponent<GameOverController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void manageDeath(){
        animator.SetTrigger("death");
        manager.decreseLives();
        Debug.Log("Vidas: " + manager.getLives());
        if(manager.getLives() <= 0){
            Debug.Log("GAME OVER");
            gameoverRendererController.setActive(true);
        }
        else{
            renderLives();
            StartCoroutine(respawnCharacter());
        }
    }

    private IEnumerator respawnCharacter(){
        Debug.Log("Respawn Character at " + manager.getRespawnPoint());
        yield return new WaitForSeconds(1);
        animator.SetTrigger("iddle");
        transform.position = manager.getRespawnPoint();
    }

    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.transform.tag == "Trampa"){
            Debug.Log("TRAMPA");
            manageDeath();
        }
        else if(collider.tag == "FallDetector"){
            Debug.Log("Caída");
            manageDeath();
        }
    }

    private void renderScore(){
        statsRendererController.updateScore(manager.getScore());
    }
    private void renderLives(){
        int lives = manager.getLives();
        statsRendererController.updateLives((lives>=1?true:false,lives>=2?true:false,lives>=3?true:false));
    }
    private void renderItems(){
        statsRendererController.updateItems(manager.getItems());
    }
    private void renderPowers(){
        statsRendererController.updatePowers(manager.getPowers());
    }
}
