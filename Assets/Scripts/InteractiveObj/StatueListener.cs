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
            Transform childrenObject = transform.Find("statue");
            if (childrenObject != null)
            {
                Transform grandChildrenObject = childrenObject.Find(handObjs.name);
                if (grandChildrenObject != null)
                {
                    GameObject isStatue = grandChildrenObject.gameObject;
                    Destroy(pickUpObj.handObj.gameObject);
                    pickUpObj.handObj = null;
                    pickUpObj.handEmpty = true;
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
