using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Interactive : PickAndInteractiveFather
{
    public delegate void InteractiveEventDelegate();
    public InteractiveEventDelegate eventRotateWall;
    public InteractiveEventDelegate eventDoPuzzle;
    public InteractiveEventDelegate eventTurnOnLight;

    //把花放到案板上
    public InteractiveEventDelegate eventPutFlower;
    public InteractiveEventDelegate eventMakeTea;
    public InteractiveEventDelegate eventPutPot;

    public InteractiveEventDelegate eventSitOnObject;
    public InteractiveEventDelegate eventMBDoorOpen;
    public InteractiveEventDelegate eventMoveDrawer;

    public Transform rotateWall;

    public Bag myBag;



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
            

            eventTurnOnLight?.Invoke();
            

            if (pickObj.name == "StatuePos")
            {
                eventDoPuzzle?.Invoke();
            }
            
            
            eventPutFlower?.Invoke();
            
            
            
            eventPutPot?.Invoke();
            
            
            
            eventMakeTea?.Invoke();
            


                //eventTurnOnLight?.Invoke();
            
            
                eventDoPuzzle?.Invoke();
            
            
            
                eventMoveDrawer?.Invoke();
            if (pickObj.name == "sofa2")
            {
                eventSitOnObject?.Invoke();
            }
            if (pickObj.name == "MBDoor" || pickObj.name == "MBDoor1"||pickObj.name=="StudioDoor")
            {
                eventMBDoorOpen?.Invoke();
            }




        }
    }

}
