using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{    
    [SerializeField] private GameObject pauseMenu;  // Asignar el panel PauseMenu a este objeto desde 
                                                    // el elemento 'script' en el inspector, una vez
                                                    // este script se haya asociado a los botones.
    
    private bool paused = false;
    
    void Update()
    {   // La tecla ESC controla la pausa
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(paused){
                Resume();
            }
            else{
                Pause();
            }
        }
    }
    
    public void Pause(){
        Debug.Log("PAUSE");
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        paused = !paused;
    }

    public void Resume(){
        Debug.Log("PLAY");
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = !paused;
    }

    public void goMenu(int _escena){
        Debug.Log("BUTTON -> Menú");
        Time.timeScale = 1f;
        SceneManager.LoadScene(_escena);
    }
}
