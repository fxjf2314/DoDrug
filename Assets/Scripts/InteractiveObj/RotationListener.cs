using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationListener : Listener
{
    
    public float duration = 1.0f; // 旋转持续时间，单位秒
    private float rotationAngle = 90.0f; // 每次旋转的角度
    private float totalRotation = 180.0f; // 累积的总旋转角度

    protected override void Start()
    {
        base.Start();
        somethingScript = objWithInteractive.GetComponent<Interactive>();
        somethingScript.eventRotateWall += Dosomething;
    }

    

    protected override void Dosomething()
    {
        // 调用旋转方法，每次旋转后更新总旋转角度
        RotateAroundYAxis(totalRotation, duration);
        totalRotation += rotationAngle;
    }

    private void RotateAroundYAxis(float totalAngle, float duration)
    {
        // 计算旋转的四元数
        Quaternion toRotation = Quaternion.Euler(0f, totalAngle, 0f);
        // 使用Coroutine来实现平滑旋转
        StartCoroutine(SmoothRotate(toRotation, duration));
    }

    private IEnumerator SmoothRotate(Quaternion toRotation, float duration)
    {
        Quaternion fromRotation = transform.rotation;
        float time = 0f;

        while (time < duration)
        {
            // 计算当前旋转的插值
            transform.rotation = Quaternion.Slerp(fromRotation, toRotation, time / duration);
            // 增加时间
            time += Time.deltaTime;
            yield return null;
        }

        // 确保旋转到精确的角度
        transform.rotation = toRotation;
    }
}
