using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class CrouchShootState : State
    {
        // constructor
        public CrouchShootState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            player.anim.Play("arthur_stand", 0, 0);
            
            player.CheckForLand();
            
            player.xv = player.yv = 0;
        }

        public override void Exit()
        {
            base.Exit();

            player.anim.SetBool("stand", false );
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {

            //base.LogicUpdate();
            /*
            Can:
            Stand Attack
            Move
            Jump
            Crouch
            */
            

            if(player.crouchButtonReleased)
                sm.ChangeState(player.standingState);
            
            if(player.shootButtonReleased)
                sm.ChangeState(player.crouchingState);
            
        }
    }
}
