using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MoveState : Player_GroundeState
{
    public Player_MoveState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }


    public override void Update()
    {
        base.Update();

        if (player.moveInput.x == 0)
            StateMachine.ChangeState(player.idleState);

        //人物转向
        /*
        else if (player.moveInput.x > 0 && player.facingRight != true)
            player.Fild();
        else if (player.moveInput.x < 0 && player.facingRight)
            player.Fild();
        */

        player.SetVelocity(player.moveInput.x * player.moveSpeed, rb.velocity.y);
    }
}
