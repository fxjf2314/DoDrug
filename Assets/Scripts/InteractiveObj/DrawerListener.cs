using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerListener : Listener
{
    public float moveDistance = 5.0f; // �ƶ��ľ���
    public float duration = 1.0f; // �ƶ�����ʱ�䣬��λ��
    public Vector3 moveDirection = Vector3.forward; // �ƶ��ķ���Ĭ����ǰ

    private Vector3 startPosition; // ��ʼλ��
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
            // ���㵱ǰλ�õĲ�ֵ
            transform.position = Vector3.Lerp(startPosition, startPosition + moveDirection * moveDistance, (elapsedTime / duration));
            // �����Ѿ���ȥ��ʱ��
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ȷ���ƶ�����ȷ��λ��
        transform.position = startPosition + moveDirection * moveDistance;
        transform.tag = "Untagged";
    }
}
