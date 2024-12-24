using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : PickAndInteractiveFather
{
    //视角摄像机及物品拾取

    public bool handEmpty;
    public Transform handObj;
    public Vector3 handPos;
    public TextMeshProUGUI tips;
    //pickObj物体的描边组件
    Rigidbody rb;
    

    protected override void Awake()
    {
        tagName = "PickObj";
        base.Awake();
        
    }

    private void Start()
    {
        pickObj = null;
        handObj = null;
        handEmpty = true;
        //设置要忽略的层级
        
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
            if (pickObj.GetComponent<ItemOnWorld>().mybag.items.Count < 6)
            {
                if (!handEmpty && handObj != null)
                {
                    handObj.gameObject.SetActive(false);
                }
                handObj = pickObj;
                rb = handObj.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezeAll;
                GetAItem.inHandObj = handObj;
                handObj.GetComponent<ItemOnWorld>().AddNewItem();
                ChangeLayer(handObj);
                //设置transform参数确保手中物品角度正确不挡视野
                handObj.SetParent(Cam.transform);
                handObj.localEulerAngles = Vector3.zero;
                handObj.localPosition = handPos;
                //关闭描边
                CloseOutline();
                handEmpty = false;
            }
            else
            {
                tips.gameObject.SetActive(true);
            }
            //handObj = pickObj;
        }
    }

    void ChangeLayer(Transform handObj)
    {
        handObj.gameObject.layer = LayerMask.NameToLayer("Player");
        for (int i = 0; i < handObj.childCount; i++)
        {
            Transform child = handObj.GetChild(i);
            child.gameObject.layer = LayerMask.NameToLayer("Player");
            ChangeLayer(child);
        }
    }

    /*void GetPickObj()
    {
        //从视角发射一条射线
        pickRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        
        if(Physics.Raycast(pickRay,out hit,rayDistance, ignoreLayer))
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
        Debug.DrawLine(pickRay.origin, pickRay.origin + pickRay.direction * rayDistance, Color.red);
    }*/
    /*//开启描边
    void OpenOutline(RaycastHit hit)
    {
        pickObj = hit.transform;
        outline = hit.transform.GetComponent<Outline>();
        outline.enabled = true;
        pickUI.SetActive(true);
    }
    //关闭描边
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
    }*/
}
