using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class MovingState : State
    {
        private int playerDir;
        // constructor
        public MovingState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            player.anim.Play("arthur_run", 0, 100);
            
            //player.CheckForLand();
            
            //player.xv = player.yv = 0;
        }

        public override void Exit()
        {
            base.Exit();

            //player.anim.SetBool("stand", true );
        }

        public override void HandleInput()
        {
            base.HandleInput();
            
            if(Input.GetKey(KeyCode.LeftArrow))
            {
                playerDir = -8;
                player.sh.SetSpriteXDirection(Dir.Left);
            }

            if(Input.GetKey(KeyCode.RightArrow))
            {
                player.sh.SetSpriteXDirection(Dir.Right);
                playerDir = 8;
            }

            if(!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                playerDir = 0;
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
            

            if(player.crouchButtonPressed)
                sm.ChangeState(player.crouchingState);
            
            if(player.jumpButtonPressed)
                sm.ChangeState(player.jumpingState);

            if(player.moveButtonReleased)
                sm.ChangeState(player.standingState);
            
            //if(player.downButtonPressed)
            //    sm.ChangeState(crouchingState);
            
                
        }
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            //Debug.Log(playerDir);
            player.xv = playerDir;
        }
    }
}
