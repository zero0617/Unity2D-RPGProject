using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//����׷������ƶ�
public class ParallaxBcakground : MonoBehaviour
{
    private Camera mainCamera;  //�����
    private float lastCameraPositionX;  //���x�����λ��
    private float cameraHalfWidth;  //���һ����

    [SerializeField] private ParallaxLayer[] backgroundLayers;

    private void Awake()
    {
        mainCamera = Camera.main;
        cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        CalculateImageLength();
    }


    private void FixedUpdate()
    {
        
        float currentCameraPositionX = mainCamera.transform.position.x; //��ʼ��ÿ֡��ǰ���xλ��   
        float distanceToMove = currentCameraPositionX - lastCameraPositionX;   //ÿ֡����ƶ��ľ���   
        lastCameraPositionX = currentCameraPositionX;   //����������λ��

        float cameraLeftEdge = currentCameraPositionX - cameraHalfWidth;    //�����߽�
        float cameraRightEdge = currentCameraPositionX + cameraHalfWidth;   //����ұ߽�

        //��ÿ����������ƶ�����
        foreach (ParallaxLayer layer in backgroundLayers)
        {
            layer.Move(distanceToMove);
            layer.LoopBackground(cameraLeftEdge, cameraRightEdge);
        }
    }

    //����ͼƬ����
    private void CalculateImageLength()
    {
        foreach(ParallaxLayer layer in backgroundLayers)
        {
            layer.CalculateImageWidth();
        }
    }
}
