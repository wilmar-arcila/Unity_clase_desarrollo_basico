using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    public void updateLives((bool live1, bool live2, bool live3)lives){
        Debug.Log("[StatsController]PANEL -> UpdateLives: " + lives);
    }

    public void updateScore(int score){
        Debug.Log("[StatsController]PANEL -> UpdateScore: " + score);
    }

    public void updateItems((int item1, int item2, int item3)items){
        Debug.Log("[StatsController]PANEL -> UpdateItems: " + items);
    }

    public void updatePowers((bool item1, bool item2, bool item3)powers){
        Debug.Log("[StatsController]PANEL -> UpdatePowers: " + powers);
    }
}
