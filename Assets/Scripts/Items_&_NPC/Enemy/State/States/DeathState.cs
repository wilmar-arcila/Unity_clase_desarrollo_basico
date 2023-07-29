using UnityEngine;

namespace Enemy.State
{

    public class DeathState : IState
    {
        private EnemyController controller;
        private Animator animator;

        public DeathState(EnemyController controller)
        {
            this.controller = controller;
            animator = controller.enemyAnimator;
        }

        public void Enter(){
            controller.enemyAnimator.SetTrigger("death");
            Debug.Log("[DeathState]Enemigo Muerto");
        }
        public void Update(){
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0)){
                controller.enemyDead = true;
            }
        }
        public void Exit(){
            Debug.Log("[DeathState]Exiting");
        }
    }

}
