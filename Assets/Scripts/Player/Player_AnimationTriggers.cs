using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//¶¯»­ÊÂ¼þ
public class Player_AnimationTriggers : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void CurrentStateTrigger()
    {
        player.CallAnimationTrigger();
    }
}
