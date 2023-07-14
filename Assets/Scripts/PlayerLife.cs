using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    private Animator animator;

    private int vidas = 3;
    private Vector3 puntoAparicion = new Vector3(0,6.1f,0);

    private void Start()
    {
        animator = GetComponent<Animator>();
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
        vidas--;
        if(vidas <= 0){
            Debug.Log("GAME OVER");
        }
        else{
            Debug.Log("Vidas: " + vidas);
        }
    }

    private void restartPosition(){
        transform.position = puntoAparicion;
    }
}