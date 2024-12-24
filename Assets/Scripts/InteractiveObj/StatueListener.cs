using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusListener : Listener//µñÏñ¼àÌý
{
    private PickUp pickUpObj;
    private GameObject handObjs;
    public TextMeshProUGUI tips;
    public Bag myBag;
    protected override void Start()
    {
        base.Start();
        pickUpObj = objWithInteractive.GetComponent<PickUp>();
        somethingScript = objWithInteractive.GetComponent<Interactive>();
        somethingScript.eventDoPuzzle += Dosomething;
    }

    protected override void Dosomething()
    {
        if(pickUpObj.handObj != null)
        {
            handObjs = pickUpObj.handObj.gameObject;
            Transform childrenObject = transform.Find("cutting board");
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
                    tips.gameObject.SetActive(true);
                }
            }
        }        
    }
}
