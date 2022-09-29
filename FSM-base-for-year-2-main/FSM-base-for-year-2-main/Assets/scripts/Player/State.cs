
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Player
{
    public abstract class State
    {
        protected static PlayerScript player;
        protected static StateMachine sm;

        
        



        // base constructor
        protected State(PlayerScript player, StateMachine sm)
        {
            State.player = player;
            State.sm = sm;
            
        }

        // These methods are common to all states
        public virtual void Enter()
        {
            //Debug.Log("This is base.enter");
        }

        public virtual void HandleInput()
        {
            player.ReadInputKeys();
        }

        public virtual void LogicUpdate()
        {
        }

        public virtual void PhysicsUpdate()
        {
        }

        public virtual void Exit()
        {
        }

    }

}