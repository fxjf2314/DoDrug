using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public delegate void InteractiveEventDelegate();
    public InteractiveEventDelegate eventRotateWall;
    public InteractiveEventDelegate eventDoPuzzle;
    public InteractiveEventDelegate eventTurnOnLight;
    public GameObject pickUI;
    Outline outline;
    bool isKeyDown;
    public Transform rotateWall;

    public PickUp PickUpObj;
    private GameObject interactiveObj;
    private void Awake()
    {
        PickUpObj = GetComponent<PickUp>();
        pickUI.SetActive(false);
    }

    

    //检测面前是否有物体，有则弹出互动提示
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InteractiveObj"))
        {
            outline = other.GetComponent<Outline>();
            outline.enabled = true;
            //pickObj = other.GetComponent<Transform>();
            //picktmp.enabled = true;
            interactiveObj = other.gameObject;
            pickUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("InteractiveObj"))
        {
            if (outline != null)
            {
                outline.enabled = false;
                outline = null;
            }
            //pickObj = null;
            //picktmp.enabled = false;
            interactiveObj = null;
            pickUI.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && pickUI.activeSelf)
        {
            if(interactiveObj.name == "StudioLight")
            {
                eventTurnOnLight?.Invoke();
            }
            if (interactiveObj.name == "StatuePos" && PickUpObj.handObj != null)
            {
                eventDoPuzzle?.Invoke();
            }
            eventRotateWall?.Invoke();
        }
    }

}
