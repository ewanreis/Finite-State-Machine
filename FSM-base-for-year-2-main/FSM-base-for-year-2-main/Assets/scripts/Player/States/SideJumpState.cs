using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class SideJumpState : State
    {
    // constructor
        public SideJumpState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.anim.Play("arthur_jump_up", 0, 0);
            player.xv = (player.sh.GetDirection() == Dir.Left) ? -5 : 5;
            player.yv = 15;
            player.sh.SetDynamic(player.rb);
            //player.rb.AddForce(new Vector2(0, 10000), ForceMode2D.Impulse);
            player.StartCoroutine(player.JumpUpCoroutine());
            
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
            base.LogicUpdate();
            if(player.onPlatform && !player.jumpFlag)
                sm.ChangeState(player.standingState);
        }
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            if(!player.onPlatform)
                player.jumpFlag = false;
        }
    }
}


