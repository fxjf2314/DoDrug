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
            
            
                eventDoPuzzle?.Invoke();
            
            
            
                eventMoveDrawer?.Invoke();
            
            

        }
    }

}
