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
        //���ӽǷ���һ������

        pickRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Debug.DrawLine(pickRay.origin, pickRay.origin + pickRay.direction * rayDistance, Color.red);
        if (Physics.Raycast(pickRay, out hit, rayDistance, ignoreLayer))
        {
            //�����ǰ�Ƿ��п�ʰȡ���壬���򵯳�ʰȡ��ʾ,�������
            if (hit.collider.CompareTag(tagName))
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

    protected void OpenOutline(RaycastHit hit)
    {
        pickObj = hit.transform;
        outline = hit.transform.GetComponent<Outline>();
        outline.enabled = true;
        pickUI.SetActive(true);
    }
    //�ر����
    protected void CloseOutline()
    {
        pickObj = null;
        //��һ֡û�п����ʰȡ����ֱ��return
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
