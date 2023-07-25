using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject defaultCharacter;
    private GameObject actualCharacter; // Mantiene el Prefab del personaje con el que se desea jugar

    //////////////////////////////////////////////
    /*          SINGLETON PATTERN               */
    private static GameManager Instance;
    private void Awake()
    {
        if(GameManager.Instance == null){
            GameManager.Instance = this;
            actualCharacter = defaultCharacter;
            DontDestroyOnLoad(this);
        }
        else{
            Destroy(this);
        }
    }
    public static GameManager getInstance(){
        return GameManager.Instance;
    }
    ///////////////////////////////////////////////

    public void setCharacter(GameObject character){
        actualCharacter = character;
    }
    public GameObject getCharacter(){
        return actualCharacter;
    }
}
