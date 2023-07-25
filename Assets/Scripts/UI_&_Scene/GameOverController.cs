using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    private GameObject gameOverPanel;

    private CharacterStatsManager manager;

    //////////////////////////////////////////////
    /*          OBSERVER PATTERN (as Observer)  */
    public void Start() {
        gameOverPanel = transform.GetChild(0).gameObject;
        manager = CharacterStatsManager.getInstance();
        if (manager != null) // Se suscribe a los respectivos eventos
        {
            manager.CharacterGameOver += OnCharacterGameOver;
        }
    }
    private void OnDestroy()
    {
        if (manager != null) // Cancela la suscripción a los eventos
        {
            manager.CharacterGameOver -= OnCharacterGameOver;
        }
    }

    public void OnCharacterGameOver(){
        gameOverPanel.SetActive(true);
    }
}
