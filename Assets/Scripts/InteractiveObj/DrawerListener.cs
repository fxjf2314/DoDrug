using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerListener : Listener
{
    public float moveDistance = 5.0f; // 移动的距离
    public float duration = 1.0f; // 移动持续时间，单位秒
    public Vector3 moveDirection = Vector3.forward; // 移动的方向，默认向前

    private Vector3 startPosition; // 起始位置
    private float elapsedTime = 0f;

    protected override void Start()
    {
        base.Start();
        somethingScript = objWithInteractive.GetComponent<Interactive>();
        somethingScript.eventMoveDrawer += Dosomething;
        startPosition = transform.position;
    }

    protected override void Dosomething()
    {
        if (PickAndInteractiveFather.pickObj.name == transform.name)
        {
            StartCoroutine(SmoothMove());
        }
           
    }

    protected IEnumerator SmoothMove()
    {
        while (elapsedTime < duration)
        {
            // 计算当前位置的插值
            transform.position = Vector3.Lerp(startPosition, startPosition + moveDirection * moveDistance, (elapsedTime / duration));
            // 增加已经过去的时间
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保移动到精确的位置
        transform.position = startPosition + moveDirection * moveDistance;
        transform.tag = "Untagged";
    }
}
