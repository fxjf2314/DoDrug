using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public delegate void InteractiveEventDelegate();
    public InteractiveEventDelegate eventRotateWall;
    public GameObject pickUI;
    Outline outline;
    bool isKeyDown;
    public Transform rotateWall;

    private void Awake()
    {
        
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
            pickUI.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && pickUI.activeSelf)
        {
            eventRotateWall?.Invoke();
        }
    }

}
