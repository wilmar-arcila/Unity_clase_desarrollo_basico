using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLive : MonoBehaviour
{
    [SerializeField] private int totalLives;

    private int lives;


    void Start()
    {
        lives = totalLives;
    }

    public int getLives(){
        return lives;
    }

    public int setLives(int _lives){
        lives = _lives;
        return lives;
    }
}
