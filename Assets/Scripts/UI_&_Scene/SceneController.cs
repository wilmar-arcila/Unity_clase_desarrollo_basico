using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
   void Start() {
      Debug.Log("ENTERING ESCENE: " + SceneManager.GetActiveScene().buildIndex);
   }

   public void ReloadScene(){
      ChangeScene(SceneManager.GetActiveScene().buildIndex);
   }
   
   public void ChangeScene(int _escena){
      Debug.Log("CALL -> changeScene: " + _escena);
      
      if(!TryGetComponent<AudioSource>(out var audio))
      {   // Si el objeto NO TIENE componente de AUDIO se cambia de escena inmediatamente
         SceneManager.LoadScene(_escena);
      }
      else{                // Si el objeto SI TIENE componente de AUDIO solo se cambia de escena cuando el audio haya terminado de reproducirse
         Debug.Log("[changeScene] AudioSource:  " + audio);
         StartCoroutine(ChangeSceneWithAudio(_escena, audio));
      }
   }

   private IEnumerator ChangeSceneWithAudio(int _escena, AudioSource audio){
      Debug.Log("[changeScene]Yield Init");
      yield return new WaitUntil(() => audio.isPlaying == false); // Se ejecuta esta línea hasta que la condición sea verdadera
      Debug.Log("[changeScene]Yield Over");
      SceneManager.LoadScene(_escena);
   }

   public void QuitGame(){
      #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
      #endif
      Application.Quit();
   }

}