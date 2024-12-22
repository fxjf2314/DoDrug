using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    //�ӽ����������Ʒʰȡ
    public Camera Cam;
    //��ǰ��ʰȡ����Ʒ��������Ʒ
    Transform pickObj;
    Transform handObj;
    bool handEmpty;
    public GameObject pickUI;
    public Vector3 handPos;
    //pickObj�����������
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
            if (!handEmpty)
            {
                handObj.gameObject.SetActive(false);
            }
            handObj = pickObj;
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

    void GetPickObj()
    {
        //���ӽǷ���һ������
        pickRay = Camera.main.ScreenPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
        RaycastHit hit;
        if(Physics.Raycast(pickRay,out hit,rayDsitance))
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
    }
}
