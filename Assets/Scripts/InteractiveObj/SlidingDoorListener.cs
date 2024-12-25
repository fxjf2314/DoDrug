using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoorListener : DrawerListener
{
    private PickUp pickUpObj;
    public Bag myBag;
    protected override void Start()
    {
        
        base.Start();
        pickUpObj = objWithInteractive.GetComponent<PickUp>();


    }

    protected override void Dosomething()
    {
        if(pickUpObj.handObj != null && pickUpObj.handObj.name == PickAndInteractiveFather.pickObj.name)
        {
            Debug.Log("11111");
            StartCoroutine(SmoothMove());

            
        }
        
    }
}
