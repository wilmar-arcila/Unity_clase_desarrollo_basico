using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevoPersonaje", menuName = "Playables/Personaje", order = 1)]

public class Character : ScriptableObject
{
    public GameObject personaje;

    public Sprite imagenPersonaje;

    public string nombrePersonaje;
}
