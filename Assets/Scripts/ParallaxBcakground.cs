using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//背景追随相机移动
public class ParallaxBcakground : MonoBehaviour
{
    private Camera mainCamera;  //主相机
    private float lastCameraPositionX;  //相机x的最后位置
    private float cameraHalfWidth;  //相机一半宽度

    [SerializeField] private ParallaxLayer[] backgroundLayers;

    private void Awake()
    {
        mainCamera = Camera.main;
        cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        CalculateImageLength();
    }


    private void FixedUpdate()
    {
        
        float currentCameraPositionX = mainCamera.transform.position.x; //初始化每帧当前相机x位置   
        float distanceToMove = currentCameraPositionX - lastCameraPositionX;   //每帧相机移动的距离   
        lastCameraPositionX = currentCameraPositionX;   //更新相机最后位置

        float cameraLeftEdge = currentCameraPositionX - cameraHalfWidth;    //相机左边界
        float cameraRightEdge = currentCameraPositionX + cameraHalfWidth;   //相机右边界

        //给每个背景添加移动方法
        foreach (ParallaxLayer layer in backgroundLayers)
        {
            layer.Move(distanceToMove);
            layer.LoopBackground(cameraLeftEdge, cameraRightEdge);
        }
    }

    //计算图片长度
    private void CalculateImageLength()
    {
        foreach(ParallaxLayer layer in backgroundLayers)
        {
            layer.CalculateImageWidth();
        }
    }
}
