using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CuttingListener : Listener
{
    private PickUp pickUpScript;
    private Transform handObj;
    public TextMeshProUGUI tips;
    public Bag myBag;
    //�����ϵĻ�
    Transform flower;
    //�����ϵĻ���
    Transform pollen;

    protected override void Start()
    {
        base.Start();
        pollen = transform.Find("Pollen");
        pickUpScript = objWithInteractive.GetComponent<PickUp>();
        somethingScript = objWithInteractive.GetComponent<Interactive>();
        somethingScript.eventPutFlower += Dosomething;
    }

    protected override void Dosomething()
    {
        //��ȡ��������
        handObj = pickUpScript.handObj;
        if(handObj == null)//����û����Ʒ�򵯳���ʾ
        {
            tips.text = "Nothing can put on the cutting board";
            tips.gameObject.SetActive(true);
        }
        else
        {
            if(handObj.name == "Flor Pet")//������Ʒ�ǻ����򽫻���ŵ�������
            {
                flower = handObj.transform;
                //�Ӱ����Ƴ���Ӧ���� 
                myBag.items.Remove(flower.GetComponent<ItemOnWorld>().thisItem);
                BagManager.RemoveItemSlot(flower.gameObject.GetComponent<ItemOnWorld>().thisItem);
                //��Ӧ�����ÿ�
                handObj = null;
                GetAItem.inHandObj = null;
                pickUpScript.handEmpty = true;
                flower.SetParent(transform);
                //����λ��
                flower.localPosition = new Vector3(0, 0, 0.0007f);
                
            }
            else if(handObj.name == "Knife" && transform.Find("Flor Pet"))//������Ʒ�ǲ˵��Ұ������л����򽫻���ݻ٣����ۼ���
            {
                Destroy(flower.gameObject);
                if(pollen != null)
                {
                    pollen.gameObject.SetActive(true);
                }
                transform.tag = "Untagged";
            }
            else
            {
                tips.text = "This object should not be placed here";
                tips.gameObject.SetActive(true);
            }
        }
    }
}
