using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{    
    private static GameObject pauseMenu;
    
    private static bool paused = false;

    void Start()
    {
        if(PauseController.pauseMenu == null){ // Al parecer cada que se activa el panel el objeto vuelve a instanciarse
            pauseMenu = transform.GetChild(0).gameObject;
            Debug.Log("pauseMenu: " + PauseController.pauseMenu);
        }
        
        Time.timeScale = PauseController.paused?0f:1f;
    }
    
    void Update()
    {   // La tecla ESC controla la pausa
        if(Input.GetKeyDown(KeyCode.Escape)){
            Debug.Log("Key: ESC");
            if(PauseController.paused){
                Resume();
            }
            else{
                Pause();
            }
        }

        Time.timeScale = PauseController.paused?0f:1f;
    }

    public static bool isPaused(){
        return PauseController.paused;
    }

    public static void setPaused(bool _paused){
        PauseController.paused = _paused;
    }
    
    public void Pause(){
        Debug.Log("PAUSE");
        PauseController.pauseMenu.SetActive(true);
        PauseController.paused = !PauseController.paused;
        Debug.Log("Paused: " + PauseController.paused);
    }

    public void Resume(){
        Debug.Log("RESUME");
        PauseController.pauseMenu.SetActive(false);
        PauseController.paused = !PauseController.paused;
        Debug.Log("Paused: " + PauseController.paused);
    }
}
