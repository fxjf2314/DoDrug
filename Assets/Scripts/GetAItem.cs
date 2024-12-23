using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAItem : MonoBehaviour
{
    public static Transform bagObj;
    public static Transform inHandObj;
    private PickUp myHandObj;
    
    public void buttonOnClicked()
    {
        myHandObj = GameObject.Find("Main Camera").GetComponent<PickUp>();
        if(inHandObj != null )
        {
            inHandObj.gameObject.SetActive(false);
            
        }
        bagObj.gameObject.SetActive(true);
        myHandObj.handObj = bagObj;
        inHandObj = bagObj;
    }
}
