using System;
using UnityEngine;

namespace Enemy.State
{
    
    // handles
    [Serializable]
    public class StateMachine
    {
        public IState CurrentState; //{ get; private set; }

        // ESTADOS
        public IdleState idleState;
        public HurtState hurtState;
        public DeathState deathState;

        // Evento que notifica del cambio de estado
        public event Action<IState> stateChanged;

        // Es necesario pasar la referencia del controlador al cual ésta máquina da servicio
        public StateMachine(EnemyController controller)
        {
            // Se crea una instancia de cada estado que la máquina va a controlar
            idleState = new IdleState(controller);
            hurtState = new HurtState(controller);
            deathState = new DeathState(controller);
        }

        // Inicialización de la máquina de estados
        public void Initialize(IState state)
        {
            CurrentState = state;
            state.Enter();
            stateChanged?.Invoke(state);
        }

        // Método que permite indicar al amáquina de estados que debe cambiar de estado
        public void TransitionTo(IState nextState)
        {
            CurrentState.Exit();
            CurrentState = nextState;
            CurrentState.Enter();
            stateChanged?.Invoke(nextState);
        }

        // Se llama en cada frame desde el controlador
        public void Update()
        {
            if (CurrentState != null)
            {
                CurrentState.Update();
            }
        }
    }

}