using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightListener : Listener
{
    protected override void Start()
    {
        base.Start();
        somethingScript = objWithInteractive.GetComponent<Interactive>();
        somethingScript.eventTurnOnLight += Dosomething;
    }

    protected override void Dosomething()
    {
        Transform childrenObject = transform.Find("light");
        if (childrenObject != null)
        {
            Transform grandChildrenObject = childrenObject.Find("CommonLight");
            if (grandChildrenObject != null)
            {
                GameObject isLight = grandChildrenObject.gameObject;
                isLight.SetActive(true);
            }
        }
    }

}
    

