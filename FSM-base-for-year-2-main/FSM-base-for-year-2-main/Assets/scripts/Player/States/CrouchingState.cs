using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class CrouchingState : State
    {
    // constructor
        public CrouchingState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.anim.Play("arthur_crouch", 0, 0);
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
            if(player.crouchButtonReleased)
                sm.ChangeState(player.standingState);
            if(player.shootButtonPressed)
                sm.ChangeState(player.crouchShootState);
        }
    }
}

