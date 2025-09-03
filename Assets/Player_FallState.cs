using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_FallState : EntityState
{
    public Player_FallState(Player player, StateMachine stateMachine, string animaBoolName) : base(player, stateMachine, animaBoolName)
    {
    }


    public override void Update()
    {
        base.Update();

    }
}
