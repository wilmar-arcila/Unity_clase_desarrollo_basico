using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    private EnemyLive enemyLive;

    [SerializeField] private AudioSource hit_SFX;
    [SerializeField] private AudioSource attack_SFX;
    [SerializeField] private AudioSource cast_SFX;
    [SerializeField] private AudioSource spell_SFX;

    void Start()
    {
        animator = GetComponent<Animator>();
        enemyLive = GetComponent<EnemyLive>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("Player")){
            Debug.Log("[EnemyController]Hit");
            animator.SetTrigger("hurt");
        }
    }
}
