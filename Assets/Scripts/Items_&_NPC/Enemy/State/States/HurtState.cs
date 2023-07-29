using UnityEngine;

namespace Enemy.State
{

    public class HurtState : IState
    {
        private EnemyController controller;
        private Animator animator;

        public HurtState(EnemyController controller)
        {
            this.controller = controller;
            animator = controller.enemyAnimator;
        }

        public void Enter(){
            animator.SetTrigger("hurt");
            controller.decreaseEnemyLive();
        }
        public void Update(){
            Debug.Log("[HurtState]Update");
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0)){
                // La animación ya terminó
                if(controller.enemyLive.lives <= 0){
                    controller.enemyStateMachine.TransitionTo(controller.enemyStateMachine.deathState);
                }
                else{
                    controller.enemyStateMachine.TransitionTo(controller.enemyStateMachine.idleState);
                }
            }
        }
        public void Exit(){
            Debug.Log("[HurtState]Exiting");
            controller.enemyHitted = false;
        }
    }

}
