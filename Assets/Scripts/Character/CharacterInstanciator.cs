using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInstanciator : MonoBehaviour
{
    private GameManager manager;

    void Start()
    {
        manager = GameManager.getInstance();
        int indexJugador = PlayerPrefs.GetInt("JugadorIndex");
        Instantiate(manager.getCharacter(), transform.position, Quaternion.identity);
    }
}