using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEngine : MonoBehaviour
{

    //////////////////////////////////////////////
    /*         OBSERVER PATTERN (as Publisher)  */
    public event Action<(int,int,int)> CharacterItemsChanged;
    public event Action<(int,int,int)> CharacterPowersChanged;
    public event Action<int> CharacterLivesChanged;
    //////////////////////////////////////////////

    private void OnTriggerEnter2D(Collider2D collider){
        switch(collider.tag){
            case "Trampa":
                Debug.Log("[InteractionEngine]TRAMPA");
                CharacterLivesChanged?.Invoke(-1);
                break;
            case "FallDetector":
                Debug.Log("Caída");
                CharacterLivesChanged?.Invoke(-1);
                break;
            case "Collectable1":
                Debug.Log("[InteractionEngine]Collectable1");
                CharacterItemsChanged?.Invoke((1,0,0));
                break;
            case "Collectable2":
                Debug.Log("[InteractionEngine]Collectable2");
                CharacterItemsChanged?.Invoke((0,1,0));
                break;
            case "Collectable3":
                Debug.Log("[InteractionEngine]Collectable3");
                CharacterItemsChanged?.Invoke((0,0,1));
                break;
            case "Power1":
                Debug.Log("[InteractionEngine]Power1");
                CharacterPowersChanged?.Invoke((1,0,0));
                break;
            case "Power2":
                Debug.Log("[InteractionEngine]Power2");
                CharacterPowersChanged?.Invoke((0,1,0));
                break;
            case "Power3":
                Debug.Log("[InteractionEngine]Power3");
                CharacterPowersChanged?.Invoke((0,0,1));
                break;
        }
    }
}
