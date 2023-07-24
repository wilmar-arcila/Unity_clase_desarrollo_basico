using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance;

    public List<Character> personajes;

    //SINGLETON PATTERN
    private void Awake() {
        if(GameManager.Instance == null){
            GameManager.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    public static GameManager getInstance(){
        return GameManager.Instance;
    }
}
