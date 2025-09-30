using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

//����׷������ƶ�����������
public class ParallaxLayer {

    [SerializeField] private Transform background;  //��������
    [SerializeField] private float parallaxMultiplier;  //�����ƶ��ٶ�
    [SerializeField] private float imageWidthOffset = 10;    //������������������

    private float imageFullWidth;   //����ȫ��
    private float imageHalfWidth;   //�������

    //�����ƶ�
    public void Move (float distanceToMove)
    {
        //background.position = background.position + new Vector3(distanceToMove * parallaxMultiplier, 0);
        background.position += Vector3.right * (distanceToMove * parallaxMultiplier);
    }

    //���㱳�����
    public void CalculateImageWidth()
    {
        imageFullWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;
        imageHalfWidth = imageFullWidth / 2;
    }


    //����ѭ��
    public void LoopBackground(float cameraLeftEdge,float cameraRightEdge)
    {
        float imageRightEdge = (background.position.x + imageHalfWidth) - imageWidthOffset;  //�����ұ߽�
        float imageLeftEdge = (background.position.x - imageHalfWidth) + imageWidthOffset;   //������߽�

        //���������߽�С������ұ߽磬���������ƶ�һ����������
        if (imageRightEdge < cameraLeftEdge)
            background.position += Vector3.right * imageFullWidth;
        //���������߽����������ұ߽磬���������ƶ�һ����������
        else if (imageLeftEdge > cameraRightEdge)
            background.position += Vector3.right * -imageFullWidth;
            
    }

}
