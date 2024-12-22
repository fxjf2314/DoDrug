using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusListener : Listener//µñÏñ¼àÌý
{
    private PickUp pickUpObj;
    private GameObject handObjs;
    public TextMeshProUGUI tips;
    protected override void Start()
    {
        somethingScript = GameObject.Find("PickCollider").GetComponent<Interactive>();
        pickUpObj = GameObject.Find("PickCollider").GetComponent<PickUp>();
        somethingScript.eventDoPuzzle += Dosomething;
    }

    protected override void Dosomething()
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
                isStatue.SetActive(true);

            }
            else
            {
                tips.gameObject.SetActive(true);
            }
        }
    }
}
