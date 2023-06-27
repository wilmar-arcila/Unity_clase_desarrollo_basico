using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinController : MonoBehaviour
{
    [SerializeField] private AudioSource getCoin_SFX;
    
    private void OnTriggerEnter2D(Collider2D collision){
        Debug.Log("Moneda");
        getCoin_SFX.Play();

        // ESPERAR A QUE TERMINE LA REPRODUCCIÓN DEL SONIDO
        StartCoroutine(goNextLevel(getCoin_SFX.clip.length)); // Pasa el nivel con un "delay" para que alcance a sonar el audio
        gameObject.GetComponent<Renderer>().enabled = false;  // La moneda desaparece        
    }

    // Aquí está la lógica del paso de nivel después de la pausa suficiente para que suene el audio
    private IEnumerator goNextLevel(float delay){
        yield return new WaitForSeconds(delay); 
        Destroy(gameObject);

        if(SceneManager.GetActiveScene().name=="EscenaUno"){
            SceneManager.LoadScene("EscenaDos");
        }
        else{
            SceneManager.LoadScene("EscenaUno");
        }    
    }
}
