using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVListener : LightListener
{
    private PickUp pickUpObj;
    public Bag myBag;

    protected override void Start()
    {
        base.Start();
        pickUpObj = objWithInteractive.GetComponent<PickUp>();

    }
    private void Update()
    {
        if (pickUpObj.handObj != null) 
        {
            if (pickUpObj.handObj.name == transform.name)
            {
                transform.tag = "InteractiveObj";
            }
            else
            {
                transform.tag = "Untagged";
            }
        }
        
    }

    protected override void Dosomething()
    {
        if (pickUpObj.handObj != null && pickUpObj.handObj.name == PickAndInteractiveFather.pickObj.name)
        {
            Transform childrenObject = transform.Find("CameraPic");
            if (childrenObject != null)
            {
                GameObject isLight = childrenObject.gameObject;
                isLight.SetActive(true);

                Destroy(pickUpObj.handObj.gameObject);
                myBag.items.Remove(pickUpObj.handObj.gameObject.GetComponent<ItemOnWorld>().thisItem);
                BagManager.RemoveItemSlot(pickUpObj.handObj.gameObject.GetComponent<ItemOnWorld>().thisItem);
                pickUpObj.handObj = null;
                pickUpObj.handEmpty = true;
                GetAItem.inHandObj = null;
            }
        }

       

    }
}
