using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Player"){
            collider.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collider){
        if(collider.tag == "Player"){
            collider.transform.SetParent(null);
        }
    }
}
