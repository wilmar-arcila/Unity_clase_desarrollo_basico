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
        manager = GameManager.GetInstance();
        statsManager = CharacterStatsManager.GetInstance();
        int indexJugador = PlayerPrefs.GetInt("JugadorIndex");
        Debug.Log("JugadorIndex: " + indexJugador);
        Debug.Log("Jugador: " + manager.Character);
        Debug.Log("RespawnPoint: " + statsManager.GetRespawnPoint());
        character = Instantiate(manager.Character, statsManager.GetRespawnPoint(), Quaternion.identity);
        character.GetComponent<CharacterController>().SetMask(LayerMask.GetMask("Wall"));
    }

    public GameObject GetCharacter(){
        return character;
    }
}