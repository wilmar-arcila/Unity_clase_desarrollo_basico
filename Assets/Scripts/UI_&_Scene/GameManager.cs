using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject defaultCharacter;
    public GameObject Character {get; set;} // Mantiene el Prefab del personaje con el que se desea jugar

    public bool LockCharacterStats {get; set;} // ¿Se deben reiniciar las estadísticas del personaje?

    //////////////////////////////////////////////
    /*          SINGLETON PATTERN               */
    private static GameManager Instance;
    private void Awake()
    {
        if(GameManager.Instance == null){
            GameManager.Instance = this;
            Character = defaultCharacter;
            LockCharacterStats = false;
            DontDestroyOnLoad(this);
        }
        else{
            Destroy(this);
        }
    }
    public static GameManager GetInstance(){
        return GameManager.Instance;
    }
    ///////////////////////////////////////////////

}
