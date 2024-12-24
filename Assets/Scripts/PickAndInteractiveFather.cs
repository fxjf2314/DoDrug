using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PickAndInteractiveFather : MonoBehaviour
{
    public Camera Cam;
    protected Ray pickRay;
    
    public static Transform pickObj;
    public RaycastHit hit;
    protected float rayDistance = 3;
    protected int ignoreLayer;
    protected string tagName;
    protected Outline outline;
    protected int layerToIgnore;

    public GameObject pickUI;
    protected virtual void Awake()
    {
        layerToIgnore = LayerMask.NameToLayer("Player");
        ignoreLayer = ~(1 << layerToIgnore);
        pickUI.SetActive(false);
    }

    protected void GetPickObj()
    {
        //从视角发射一条射线

        pickRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Debug.DrawLine(pickRay.origin, pickRay.origin + pickRay.direction * rayDistance, Color.red);
        if (Physics.Raycast(pickRay, out hit, rayDistance, ignoreLayer))
        {
            //检测面前是否有可拾取物体，有则弹出拾取提示,开启描边
            if (hit.collider.CompareTag(tagName))
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

    protected void OpenOutline(RaycastHit hit)
    {
        pickObj = hit.transform;
        outline = hit.transform.GetComponent<Outline>();
        outline.enabled = true;
        pickUI.SetActive(true);
    }
    //关闭描边
    protected void CloseOutline()
    {
        pickObj = null;
        //上一帧没有看向可拾取物体直接return
        if (outline == null)
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
