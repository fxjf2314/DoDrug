using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    //视角摄像机及物品拾取
    public Camera Cam;
    Transform pickObj;
    Transform handObj;
    bool handEmpty;
    public GameObject pickUI;
    public Vector3 handPos;
    //
    Outline outline;

    private void Awake()
    {
        
        pickUI.SetActive(false);
    }

    private void Start()
    {
        pickObj = null;
        handObj = null;
        handEmpty = true;
        
    }

    //检测面前是否有物体，有则弹出拾取提示,开启描边
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickObj"))
        {
            pickObj = other.GetComponent<Transform>();
            outline = other.GetComponent<Outline>();
            outline.enabled = true;
            pickUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PickObj"))
        {
            pickObj = null;
            if(outline != null)
            {
                outline.enabled = false;
                outline = null;
            }
            pickUI.SetActive(false);
        }
    }

    private void Update()
    {
        //按f拾取面前物体到手中
        if (Input.GetKeyDown(KeyCode.F) && pickObj != null)
        {
            if (!handEmpty) handObj.gameObject.SetActive(false);
            handObj = pickObj;
            handObj.gameObject.layer = LayerMask.NameToLayer("Player");
            //设置transform参数确保手中物品角度正确不挡视野
            handObj.SetParent(Cam.transform);
            handObj.localEulerAngles = Vector3.zero;
            handObj.localPosition = handPos;
            //关闭描边
            if (outline != null)
            {
                outline.enabled = false;
                outline = null;
            }
            pickUI.SetActive(false);
            pickObj = null;
            handEmpty = false;
        }
    }
}
