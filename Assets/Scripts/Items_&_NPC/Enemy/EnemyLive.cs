using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLive : MonoBehaviour
{
    [SerializeField] private int totalLives;
    [SerializeField] private int decreaseRate;

    private int lives;


    void Start()
    {
        lives = totalLives;
    }

    public int getLives(){
        return lives;
    }

    public void setLives(int _lives){
        lives = _lives;
    }

    public int decreaseLives(){
        lives -= decreaseRate;
        return lives;
    }
}
