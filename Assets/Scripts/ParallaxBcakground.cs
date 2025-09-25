using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//����׷������ƶ�
public class ParallaxBcakground : MonoBehaviour
{
    private Camera mainCamera;  //�����
    private float lastCameraPositionX;  //���x�����λ��

    [SerializeField] private ParallaxLayer[] backgroundLayers;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        //��ʼ��ÿ֡��ǰ���xλ��
        float currentCameraPositionX = mainCamera.transform.position.x;

        //ÿ֡����ƶ��ľ���
        float distanceToMove = currentCameraPositionX - lastCameraPositionX;

        //����������λ��
        lastCameraPositionX = currentCameraPositionX;

        //��ÿ����������ƶ�����
        foreach(ParallaxLayer layer in backgroundLayers)
        {
            layer.Move(distanceToMove);
        }
    }
}
