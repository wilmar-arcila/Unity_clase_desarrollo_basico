using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    public static CharacterStatsManager statsManager;

    private Animator animator;

    private int lifes = 3;
    private int score = 0;

    private float respawnHigh = 6f;

    private Vector3 puntoAparicion = new Vector3(0,respawnHigh,0);

    private void Start()
    {
        // Se crea un SINGLETON del objeto que mantiene las estadísticas del personaje
        if(statsManager == null){
            statsManager = this;
            DontDestroyOnLoad(this);
        }
        else{
            Destroy(this);
        }

        animator = GetComponent<Animator>();
    }

    public void setScore(int _score){
        score = _score;
    }
    public int getetScore(){
        return _score;
    }
    public int raiseScore(int _plusScore){
        score += _plusScore;
        return score;
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

    private void manageDeath(){
        animator.SetTrigger("death");
        lifes--;
        if(vidas <= 0){
            Debug.Log("GAME OVER");
        }
        else{
            Debug.Log("Vidas: " + lifes);
        }
    }

    private void restartPosition(){
        transform.position = puntoAparicion;
    }
}