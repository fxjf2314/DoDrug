using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    //�ӽ����������Ʒʰȡ
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

    //�����ǰ�Ƿ������壬���򵯳�ʰȡ��ʾ,�������
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
        //��fʰȡ��ǰ���嵽����
        if (Input.GetKeyDown(KeyCode.F) && pickObj != null)
        {
            if (!handEmpty) handObj.gameObject.SetActive(false);
            handObj = pickObj;
            pickObj.GetComponent<ItemOnWorld>().AddNewItem();
            handObj.gameObject.layer = LayerMask.NameToLayer("Player");
            //����transform����ȷ��������Ʒ�Ƕ���ȷ������Ұ
            handObj.SetParent(Cam.transform);
            handObj.localEulerAngles = Vector3.zero;
            handObj.localPosition = handPos;
            //�ر����
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
