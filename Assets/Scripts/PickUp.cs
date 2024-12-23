using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : PickAndInteractiveFather
{
    //�ӽ����������Ʒʰȡ

    public bool handEmpty;
    public Transform handObj;
    public Vector3 handPos;
    //pickObj�����������
    
    
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
        //����Ҫ���ԵĲ㼶

    }

    ////�����ǰ�Ƿ������壬���򵯳�ʰȡ��ʾ,�������
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

        //��fʰȡ��ǰ���嵽����
        if (Input.GetKeyDown(KeyCode.F) && pickObj != null)
        {
            // ��������һ��fileID
            int fileID = 725788386;

            // ��ȡ��Ӧ��GameObject
            GameObject go = EditorUtility.InstanceIDToObject(fileID) as GameObject;

            // ���GameObject�Ƿ���Ч
            if (go != null)
            {
                // ����GameObject�������ӡ������
                Debug.Log(go.name);
            }
            if (!handEmpty)
            {
                handObj.gameObject.SetActive(false);
            }
            handObj = pickObj;
            GetAItem.inHandObj = handObj;
            handObj.GetComponent<ItemOnWorld>().AddNewItem();
            handObj.gameObject.layer = LayerMask.NameToLayer("Player");
            //����transform����ȷ��������Ʒ�Ƕ���ȷ������Ұ
            handObj.SetParent(Cam.transform);
            handObj.localEulerAngles = Vector3.zero;
            handObj.localPosition = handPos;
            //�ر����
            CloseOutline();
            handEmpty = false;
        }
    }

    /*void GetPickObj()
    {
        //���ӽǷ���һ������
        pickRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        
        if(Physics.Raycast(pickRay,out hit,rayDistance, ignoreLayer))
        {
            //�����ǰ�Ƿ��п�ʰȡ���壬���򵯳�ʰȡ��ʾ,�������
            if (hit.collider.CompareTag("PickObj"))
            {
                OpenOutline(hit);
            }
            else//�ر���һ����������
            {
                CloseOutline();
            }
        }
        else//�ر���һ����������
        {
            CloseOutline();
        }
        Debug.DrawLine(pickRay.origin, pickRay.origin + pickRay.direction * rayDistance, Color.red);
    }*/
    /*//�������
    void OpenOutline(RaycastHit hit)
    {
        pickObj = hit.transform;
        outline = hit.transform.GetComponent<Outline>();
        outline.enabled = true;
        pickUI.SetActive(true);
    }
    //�ر����
    void CloseOutline()
    {
        pickObj = null;
        //��һ֡û�п����ʰȡ����ֱ��return
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
