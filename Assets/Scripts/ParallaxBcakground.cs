using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//背景追随相机移动
public class ParallaxBcakground : MonoBehaviour
{
    private Camera mainCamera;  //主相机
    private float lastCameraPositionX;  //相机x的最后位置

    [SerializeField] private ParallaxLayer[] backgroundLayers;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        //初始化每帧当前相机x位置
        float currentCameraPositionX = mainCamera.transform.position.x;

        //每帧相机移动的距离
        float distanceToMove = currentCameraPositionX - lastCameraPositionX;

        //更新相机最后位置
        lastCameraPositionX = currentCameraPositionX;

        //给每个背景添加移动方法
        foreach(ParallaxLayer layer in backgroundLayers)
        {
            layer.Move(distanceToMove);
        }
    }
}
