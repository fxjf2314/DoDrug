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
            if(pickObj.name == "cuttingboard")
            {
                eventPutFlower?.Invoke();
            }
            if (pickObj.name == "kitchen")
            {
                eventPutPot?.Invoke();
            }
            if (pickObj.name == "Pot")
            {
                eventMakeTea?.Invoke();
            }
        }
    }

}
