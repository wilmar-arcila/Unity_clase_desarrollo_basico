using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInstanciator : MonoBehaviour
{
    private GameManager manager;
    private CharacterStatsManager statsManager;

    private GameObject character;

    void Start()
    {
        manager = GameManager.getInstance();
        statsManager = CharacterStatsManager.getInstance();
        int indexJugador = PlayerPrefs.GetInt("JugadorIndex");
        Debug.Log("JugadorIndex: " + indexJugador);
        Debug.Log("Jugador: " + manager.getCharacter());
        Debug.Log("RespawnPoint: " + statsManager.getRespawnPoint());
        character = Instantiate(manager.getCharacter(), statsManager.getRespawnPoint(), Quaternion.identity);
        character.GetComponent<CharacterController>().setMask(LayerMask.GetMask("Wall"));
    }

    public GameObject getCharacter(){
        return character;
    }
}