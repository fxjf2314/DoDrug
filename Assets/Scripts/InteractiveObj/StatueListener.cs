using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class StatusListener : Listener//雕像监听
{
    public float moveDistance = 5.0f; // 移动的距离
    public float duration = 1.0f; // 移动持续时间，单位秒
    public Vector3 moveDirection = Vector3.forward; // 移动的方向，默认向前

    private Vector3 startPosition; // 起始位置
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
            // 遍历所有子物体并检查它们的激活状态
            foreach (Transform child in childTrans)
            {
                if (!child.gameObject.activeInHierarchy)
                {
                    allActive = false;
                    break; // 一旦找到非激活的子物体，就终止循环
                }
            }





            // 如果所有子物体都激活，退出循环
            if (allActive)
            {
                StartCoroutine(SmoothMove());
                yield break; // 退出协程
            }

            // 等待下一帧再检查
            yield return null;
        }
    }

    private IEnumerator SmoothMove()
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
