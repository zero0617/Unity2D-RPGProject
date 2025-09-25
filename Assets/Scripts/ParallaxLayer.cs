using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

//����׷������ƶ�����������
public class ParallaxLayer {

    [SerializeField] private Transform background;  //��������
    [SerializeField] private float parallaxMultiplier; //�����ƶ��ٶ�


    public void Move (float distanceToMove)
    {
        //background.position = background.position + new Vector3(distanceToMove * parallaxMultiplier, 0);
        background.position += Vector3.right * (distanceToMove * parallaxMultiplier);
    }

}
