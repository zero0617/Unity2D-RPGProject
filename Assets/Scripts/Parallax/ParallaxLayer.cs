using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

//背景追随相机移动参数、方法
public class ParallaxLayer {

    [SerializeField] private Transform background;  //背景对象
    [SerializeField] private float parallaxMultiplier;  //背景移动速度
    [SerializeField] private float imageWidthOffset = 10;    //填充相机背景间误差距离

    private float imageFullWidth;   //背景全宽
    private float imageHalfWidth;   //背景半宽

    //背景移动
    public void Move (float distanceToMove)
    {
        //background.position = background.position + new Vector3(distanceToMove * parallaxMultiplier, 0);
        background.position += Vector3.right * (distanceToMove * parallaxMultiplier);
    }

    //计算背景宽度
    public void CalculateImageWidth()
    {
        imageFullWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;
        imageHalfWidth = imageFullWidth / 2;
    }


    //背景循环
    public void LoopBackground(float cameraLeftEdge,float cameraRightEdge)
    {
        float imageRightEdge = (background.position.x + imageHalfWidth) - imageWidthOffset;  //背景右边界
        float imageLeftEdge = (background.position.x - imageHalfWidth) + imageWidthOffset;   //背景左边界

        //如果背景左边界小于相机右边界，背景向右移动一个背景距离
        if (imageRightEdge < cameraLeftEdge)
            background.position += Vector3.right * imageFullWidth;
        //如果背景左边界大于于相机右边界，背景向左移动一个背景距离
        else if (imageLeftEdge > cameraRightEdge)
            background.position += Vector3.right * -imageFullWidth;
            
    }

}
