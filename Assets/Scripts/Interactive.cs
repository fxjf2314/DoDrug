using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Interactive : PickAndInteractiveFather
{
    public delegate void InteractiveEventDelegate();
    public InteractiveEventDelegate eventRotateWall;
    public InteractiveEventDelegate eventDoPuzzle;
    public InteractiveEventDelegate eventTurnOnLight;
    public Transform rotateWall;

   


    
    protected override void Awake()
    {

        tagName = "InteractiveObj";
        
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
            if (pickObj.name == "StatuePos")
            {
                eventDoPuzzle?.Invoke();
            }

        }
    }

}
