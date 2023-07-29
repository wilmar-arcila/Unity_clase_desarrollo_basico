using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLive : MonoBehaviour
{
    [SerializeField] private int totalLives;
    [SerializeField] private int decreaseRate;

    public int lives {get; private set;}


    void Start()
    {
        lives = totalLives;
    }

    public int decreaseLives(){
        lives -= decreaseRate;
        return lives;
    }
}
