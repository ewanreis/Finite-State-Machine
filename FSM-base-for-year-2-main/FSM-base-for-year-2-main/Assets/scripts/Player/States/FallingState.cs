using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class FallingState : State
    {
        // constructor
        public FallingState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            player.anim.Play("arthur_fall", 0, 0);
            player.yv = -5;
            //player.CheckForLand();
            //player.sh.SetDynamic(player.rb);
            //player.rb.velocity = new Vector2(0, -1);
            
            //player.xv = player.yv = 0;
        }

        public override void Exit()
        {
            base.Exit();

            player.anim.SetBool("stand", true);
        }

        public override void LogicUpdate()
        {
            if(player.onPlatform)
                sm.ChangeState(player.standingState);
        }

    }
}
