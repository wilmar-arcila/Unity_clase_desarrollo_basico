using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInstanciator : MonoBehaviour
{
    private GameManager manager;
    private CharacterStatsManager statsManager;

    void Start()
    {
        manager = GameManager.getInstance();
        statsManager = CharacterStatsManager.getInstance();
        int indexJugador = PlayerPrefs.GetInt("JugadorIndex");
        Instantiate(manager.getCharacter(), statsManager.getRespawnPoint(), Quaternion.identity);
    }
}