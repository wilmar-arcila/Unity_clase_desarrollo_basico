using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.State
{
    
    public class EnemyController : MonoBehaviour
    {
        public EnemyLive enemyLive {get; private set;}
        public Animator enemyAnimator {get; private set;}
        public StateMachine enemyStateMachine {get; private set;}
        public bool enemyHitted {get; set;}
        public bool enemyDead {get; set;}

        [Header("Sonidos de enemigo")]
        [Tooltip("Enemigo golpeado")]
        [SerializeField] private AudioSource hit_SFX;
        [Tooltip("Ataque espada")]
        [SerializeField] private AudioSource attack_SFX;
        [Tooltip("Ataque hechizo -> Inicio")]
        [SerializeField] private AudioSource cast_SFX;
        [Tooltip("Ataque hechizo -> Golpe")]
        [SerializeField] private AudioSource spell_SFX;
        
        private void Awake()
        {
            enemyAnimator = GetComponent<Animator>();
            enemyLive = GetComponent<EnemyLive>();
            enemyHitted = false;
            enemyDead = false;
            enemyStateMachine = new StateMachine(this);
        }

        void Start()
        {
            enemyStateMachine.Initialize(enemyStateMachine.idleState);
        }

        // Update is called once per frame
        void Update()
        {
            if(enemyDead){
                Debug.Log("[EnemyController]Destroy");
                Destroy(gameObject);
                //gameObject.SetActive(false);
            }
            enemyStateMachine.Update();
        }

        public void decreaseEnemyLive(){
            Debug.Log("[EnemyController]decreaseEnemyLive");
            enemyLive.decreaseLives();
        }

        private void OnTriggerEnter2D(Collider2D collider) {
            if(collider.CompareTag("Player")){
                Debug.Log("[EnemyController]Hit");
                enemyHitted = true;
            }
        }
    }

}
