using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Interactive : PickAndInteractiveFather
{
    public delegate void InteractiveEventDelegate();
    public InteractiveEventDelegate eventRotateWall;
    public InteractiveEventDelegate eventTurnOnLight;
    public Transform rotateWall;
    PickUp pickUpScipt;
    private GameObject interactiveObj;

    
    protected override void Awake()
    {
        tagName = "InteractiveObj";
        pickUpScipt = transform.GetComponent<PickUp>();
        base.Awake();

    }

    private void Update()
    {
        GetPickObj();
        if (Input.GetKeyDown(KeyCode.G) && pickObj != null)
        {
            if(pickObj.name == "StudioLight")
            {
                eventTurnOnLight?.Invoke();
            }
        }
    }

}
