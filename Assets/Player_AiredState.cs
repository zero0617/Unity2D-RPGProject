using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AiredState : EntityState
{
    public Player_AiredState(Player player, StateMachine stateMachine, string animaBoolName) : base(player, stateMachine, animaBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        //�����������ˮƽ����
        if (player.moveInput.x != 0)
            player.SetVelocity(player.moveInput.x * (player.moveSpeed * player.inAirMoveMultipLier), rb.velocity.y);
    }
}
