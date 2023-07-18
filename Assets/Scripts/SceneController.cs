using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void changeScene(int _escena){
      Debug.Log("CALL -> changeScene: " + _escena);
      AudioSource audio = gameObject.GetComponent<AudioSource>(); //Si el objeto no tiene componente de audio esta variable será NULL
      
      if(audio == null){   // Si el objeto NO TIENE componente de AUDIO se cambia de escena inmediatamente
         SceneManager.LoadScene(_escena);
      }
      else{                // Si el objeto SI TIENE componente de AUDIO solo se cambia de escena cuando el audio haya terminado de reproducirse
         Debug.Log("[changeScene] AudioSource:  " + audio);
         StartCoroutine(changeSceneWithAudio(_escena, audio));
      }
      Debug.Log("CALL -> changeScene: " + _escena + " END");
   }

   private IEnumerator changeSceneWithAudio(int _escena, AudioSource audio){
      Debug.Log("[changeScene]Yield Init");
      yield return new WaitUntil(() => audio.isPlaying == false); // Se ejecuta esta línea hasta que la condición sea verdadera
      Debug.Log("[changeScene]Yield Over");
      SceneManager.LoadScene(_escena);
   }

   public void quitGame(){
      #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
      #endif
      Application.Quit();
   }

}