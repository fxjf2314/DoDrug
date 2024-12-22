using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationListener : Listener
{
    
    public float duration = 1.0f; // ��ת����ʱ�䣬��λ��
    private float rotationAngle = 90.0f; // ÿ����ת�ĽǶ�
    private float totalRotation = 180.0f; // �ۻ�������ת�Ƕ�

    protected override void Start()
    {
        somethingScript = GameObject.Find("PickCollider").GetComponent<Interactive>();
        somethingScript.eventRotateWall += Dosomething;
    }

    

    protected override void Dosomething()
    {
        // ������ת������ÿ����ת���������ת�Ƕ�
        RotateAroundYAxis(totalRotation, duration);
        totalRotation += rotationAngle;
    }

    private void RotateAroundYAxis(float totalAngle, float duration)
    {
        // ������ת����Ԫ��
        Quaternion toRotation = Quaternion.Euler(0f, totalAngle, 0f);
        // ʹ��Coroutine��ʵ��ƽ����ת
        StartCoroutine(SmoothRotate(toRotation, duration));
    }

    private IEnumerator SmoothRotate(Quaternion toRotation, float duration)
    {
        Quaternion fromRotation = transform.rotation;
        float time = 0f;

        while (time < duration)
        {
            // ���㵱ǰ��ת�Ĳ�ֵ
            transform.rotation = Quaternion.Slerp(fromRotation, toRotation, time / duration);
            // ����ʱ��
            time += Time.deltaTime;
            yield return null;
        }

        // ȷ����ת����ȷ�ĽǶ�
        transform.rotation = toRotation;
    }
}
