using UnityEngine;

namespace Enemy.State
{

    public class IdleState : IState
    {
        private EnemyController controller;

        public IdleState(EnemyController controller)
        {
            this.controller = controller;
        }

        public void Enter(){
            controller.enemyAnimator.SetTrigger("idle");
        }
        public void Update(){
            if(controller.enemyHitted){
                controller.enemyStateMachine.TransitionTo(controller.enemyStateMachine.hurtState);
            }
        }
        public void Exit(){
            Debug.Log("[IdleState]Exiting");
        }
    }

}
