
using UnityEngine;
namespace Player
{
    public class StandingState : State
    {
        // constructor
        public StandingState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            player.anim.Play("arthur_stand", 0, 0);
            
            //player.CheckForLand();
            //player.sh.SetKinematic(player.rb);
            
            player.xv = 0;
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
            if(player.crouchButtonPressed)
                sm.ChangeState(player.crouchingState);
            
            if(player.jumpButtonPressed)
                sm.ChangeState(player.jumpingState);

            if(!player.moveButtonReleased)
                sm.ChangeState(player.movingState);

            if(!player.onPlatform)
                sm.ChangeState(player.fallingState);
                
        }
    }
}