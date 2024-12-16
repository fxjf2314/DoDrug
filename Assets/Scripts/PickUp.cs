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
    //TextMeshPro picktmp;

    private void Awake()
    {
        pickUI.SetActive(true);
        pickUI.SetActive(false);
    }

    private void Start()
    {
        pickObj = null;
        handObj = null;
        handEmpty = true;
        //picktmp = pickUI.GetComponent<TextMeshPro>();
    }

    //检测面前是否有物体，有则弹出拾取提示
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickObj"))
        {
            pickObj = other.GetComponent<Transform>();
            //picktmp.enabled = true;
            pickUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PickObj"))
        {
            pickObj = null;
            //picktmp.enabled = false;
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
            handObj.localPosition = new Vector3(0.9f, -0.7f, 1);
            //picktmp.enabled = false;
            pickUI.SetActive(false);
            pickObj = null;
            handEmpty = false;
        }
    }
}
