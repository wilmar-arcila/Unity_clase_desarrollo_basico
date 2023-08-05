using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject gameManagement;
    private GameObject character;

    private void Start() {
        Debug.Log("[CameraController]Start: " + character);
        character = gameManagement.GetComponent<CharacterInstanciator>().GetCharacter();
        Debug.Log("[CameraController]Character: " + character);
        if(character == null){
            StartCoroutine(LateStart());
        }
    }

    private IEnumerator LateStart(){
        Debug.Log("[CameraController]LateStart");
        yield return new WaitForSeconds(0.25f);
        character = gameManagement.GetComponent<CharacterInstanciator>().GetCharacter();
        Debug.Log("[CameraController]Character: " + character);
        if(character == null){
            StartCoroutine(LateStart());
        }
    }

    void Update()
    {
        if(character == null){return;}
        transform.position = new Vector3(character.transform.position.x, transform.position.y, transform.position.z);
    }
}
