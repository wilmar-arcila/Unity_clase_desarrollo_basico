using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    [SerializeField] private AudioSource collectable_SFX;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Player")){
            GetComponent<Collider2D>().enabled=false; // Se deshabilita el collider para evitar que lo toque más de una vez en la misma acción
            Debug.Log("[CollectableController]Collectable");
            collectable_SFX.Play();
            animator.SetTrigger("collected");
            StartCoroutine(destroyObjectWithAudio(collectable_SFX));
        }
    }

    private IEnumerator destroyObjectWithAudio(AudioSource audio){
      Debug.Log("[CollectableController]Yield Init");
      yield return new WaitUntil(() => audio.isPlaying == false); // Se ejecuta esta línea hasta que la condición sea verdadera
      Debug.Log("[CollectableController]Yield Over");
      Destroy(this);
    }
}
