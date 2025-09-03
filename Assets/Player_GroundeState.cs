using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GroundeState : EntityState
{
    public Player_GroundeState(Player player, StateMachine stateMachine, string animaBoolName) : base(player, stateMachine, animaBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (input.Player.Jump.WasPressedThisFrame())
            StateMachine.ChangeState(player.jumpState);
    }
}
