using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    //视角摄像机及物品拾取
    public Camera Cam;
    //面前可拾取的物品和手中物品
    Transform pickObj;
    Transform handObj;
    bool handEmpty;
    public GameObject pickUI;
    public Vector3 handPos;
    //pickObj物体的描边组件
    Outline outline;
    [Range(0,10)]
    public float rayDsitance;
    Ray pickRay;

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

    ////检测面前是否有物体，有则弹出拾取提示,开启描边
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("PickObj"))
    //    {
    //        pickObj = other.GetComponent<Transform>();
    //        outline = other.GetComponent<Outline>();
    //        outline.enabled = true;
    //        pickUI.SetActive(true);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("PickObj"))
    //    {
    //        pickObj = null;
    //        if(outline != null)
    //        {
    //            outline.enabled = false;
    //            outline = null;
    //        }
    //        pickUI.SetActive(false);
    //    }
    //}

    private void Update()
    {
        GetPickObj();
        //按f拾取面前物体到手中
        if (Input.GetKeyDown(KeyCode.F) && pickObj != null)
        {
            if (!handEmpty)
            {
                handObj.gameObject.SetActive(false);
            }
            handObj = pickObj;
            handObj.gameObject.layer = LayerMask.NameToLayer("Player");
            //设置transform参数确保手中物品角度正确不挡视野
            handObj.SetParent(Cam.transform);
            handObj.localEulerAngles = Vector3.zero;
            handObj.localPosition = handPos;
            //关闭描边
            CloseOutline();
            handEmpty = false;
        }
    }

    void GetPickObj()
    {
        //从视角发射一条射线
        pickRay = Camera.main.ScreenPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
        RaycastHit hit;
        if(Physics.Raycast(pickRay,out hit,rayDsitance))
        {
            //检测面前是否有可拾取物体，有则弹出拾取提示,开启描边
            if (hit.collider.CompareTag("PickObj"))
            {
                OpenOutline(hit);
            }
            else//关闭上一个物体的描边
            {
                CloseOutline();
            }
        }
        else//关闭上一个物体的描边
        {
            CloseOutline();
        }
    }

    void OpenOutline(RaycastHit hit)
    {
        pickObj = hit.transform;
        outline = hit.transform.GetComponent<Outline>();
        outline.enabled = true;
        pickUI.SetActive(true);
    }

    void CloseOutline()
    {
        pickObj = null;
        //上一帧没有看向可拾取物体直接return
        if(outline == null)
        {
            return;
        }
        else
        {
            outline.enabled = false;
            outline = null;
        }
        pickUI.SetActive(false);
    }
}
