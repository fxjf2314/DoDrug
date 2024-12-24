using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class StatusListener : Listener//�������
{
    public float moveDistance = 5.0f; // �ƶ��ľ���
    public float duration = 1.0f; // �ƶ�����ʱ�䣬��λ��
    public Vector3 moveDirection = Vector3.forward; // �ƶ��ķ���Ĭ����ǰ

    private Vector3 startPosition; // ��ʼλ��
    private float elapsedTime = 0f;

    private PickUp pickUpObj;
    private GameObject handObjs;
    public TextMeshProUGUI tips;
    public Bag myBag;

    //bool allActive = false;
    protected override void Start()
    {
        base.Start();
        pickUpObj = objWithInteractive.GetComponent<PickUp>();
        somethingScript = objWithInteractive.GetComponent<Interactive>();
        somethingScript.eventDoPuzzle += Dosomething;
        StartCoroutine(isCanMove());
        startPosition = transform.position;
    }

    protected override void Dosomething()
    {
        if (PickAndInteractiveFather.pickObj.name == transform.name)
        {

            if (pickUpObj.handObj != null)

            {
                handObjs = pickUpObj.handObj.gameObject;
                Transform childrenObject = transform.Find("statue");
                if (childrenObject != null)
                {
                    Transform grandChildrenObject = childrenObject.Find(handObjs.name);
                    if (grandChildrenObject != null)
                    {
                        GameObject isStatue = grandChildrenObject.gameObject;
                        Destroy(pickUpObj.handObj.gameObject);
                        myBag.items.Remove(pickUpObj.handObj.gameObject.GetComponent<ItemOnWorld>().thisItem);
                        BagManager.RemoveItemSlot(pickUpObj.handObj.gameObject.GetComponent<ItemOnWorld>().thisItem);
                        pickUpObj.handObj = null;
                        pickUpObj.handEmpty = true;
                        GetAItem.inHandObj = null;
                        isStatue.SetActive(true);

                    }
                    else
                    {   
                        tips.text = "This object should not be placed here";
                        tips.gameObject.SetActive(true);
                        StartCoroutine(DisappearUIfunc.DisappearUI(tips));
                    }
                }
            }

        }
 
    }

    private IEnumerator isCanMove()
    {
        while (true)
        {
            bool allActive = true;
            Transform childTrans = transform.Find("statue");
            // �������������岢������ǵļ���״̬
            foreach (Transform child in childTrans)
            {
                if (!child.gameObject.activeInHierarchy)
                {
                    allActive = false;
                    break; // һ���ҵ��Ǽ���������壬����ֹѭ��
                }
            }





            // ������������嶼����˳�ѭ��
            if (allActive)
            {
                StartCoroutine(SmoothMove());
                yield break; // �˳�Э��
            }

            // �ȴ���һ֡�ټ��
            yield return null;
        }
    }

    private IEnumerator SmoothMove()
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
