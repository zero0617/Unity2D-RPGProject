using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

//背景追随相机移动参数、方法
public class ParallaxLayer {

    [SerializeField] private Transform background;  //背景对象
    [SerializeField] private float parallaxMultiplier; //背景移动速度


    public void Move (float distanceToMove)
    {
        //background.position = background.position + new Vector3(distanceToMove * parallaxMultiplier, 0);
        background.position += Vector3.right * (distanceToMove * parallaxMultiplier);
    }

}
