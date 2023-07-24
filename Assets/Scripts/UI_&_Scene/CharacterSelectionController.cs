using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectionController : MonoBehaviour
{
    private int index;

    [SerializeField] private Image imagen;
    [SerializeField] private TextMeshProUGUI nombre;

    private GameManager manager;

    void Start()
    {
        manager = GameManager.getInstance();
        index = PlayerPrefs.GetInt("JugadorIndex");

        if(index > manager.personajes.Count - 1){
            index = 0;
        }
        cambiarPantalla();
    }

    private void cambiarPantalla(){
        PlayerPrefs.SetInt("JugadorIndex", index);
        imagen.sprite = manager.personajes[index].imagenPersonaje;
        nombre.text = manager.personajes[index].nombrePersonaje;
    }

    public void siguientePersonaje(){
        if(index == manager.personajes.Count-1){
            index = 0;
        }
        else{
            index++;
        }
        cambiarPantalla();
    }
    public void anteriorPersonaje(){
        if(index == 0){
            index = manager.personajes.Count-1;
        }
        else{
            index--;
        }
        cambiarPantalla();
    }
}
