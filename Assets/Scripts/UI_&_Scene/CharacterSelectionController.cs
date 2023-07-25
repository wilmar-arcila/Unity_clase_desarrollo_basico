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

    [SerializeField] private ItemsHolder holder;

    private GameManager manager;

    void Start()
    {
        manager = GameManager.getInstance();
        index = PlayerPrefs.GetInt("JugadorIndex");

        if(index > holder.personajes.Count - 1){
            index = 0;
        }
        cambiarJugador();
    }

    private void cambiarJugador(){
        PlayerPrefs.SetInt("JugadorIndex", index);
        imagen.sprite = holder.personajes[index].imagenPersonaje;
        nombre.text = holder.personajes[index].nombrePersonaje;
        manager.setCharacter(holder.personajes[index].personaje);
    }

    public void siguientePersonaje(){
        if(index == holder.personajes.Count-1){
            index = 0;
        }
        else{
            index++;
        }
        cambiarJugador();
    }
    public void anteriorPersonaje(){
        if(index == 0){
            index = holder.personajes.Count-1;
        }
        else{
            index--;
        }
        cambiarJugador();
    }
}
