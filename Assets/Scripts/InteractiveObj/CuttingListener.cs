using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CuttingListener : Listener
{
    private PickUp pickUpScript;
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
        if (PickAndInteractiveFather.pickObj.name == transform.name)
        {
            if (pickUpScript.handObj == null)//����û����Ʒ�򵯳���ʾ
            {
                tips.text = "Nothing can put on the cutting board";
                tips.gameObject.SetActive(true);
            }
            else
            {
                if (pickUpScript.handObj.name == "Flor Pet")//������Ʒ�ǻ����򽫻���ŵ�������
                {
                    flower = pickUpScript.handObj.transform;
                    //�Ӱ����Ƴ���Ӧ���� 
                    myBag.items.Remove(flower.GetComponent<ItemOnWorld>().thisItem);
                    BagManager.RemoveItemSlot(flower.gameObject.GetComponent<ItemOnWorld>().thisItem);
                    //��Ӧ�����ÿ�
                    pickUpScript.handObj = null;
                    GetAItem.inHandObj = null;
                    pickUpScript.handEmpty = true;
                    flower.SetParent(transform);
                    //����λ��
                    flower.localPosition = new Vector3(-1.120478e-05f, -0.0003f, 0.041f);
                    flower.localEulerAngles = new Vector3(98.47299f, -84.793f, -88.49298f);
                    flower.gameObject.layer = LayerMask.NameToLayer("Default");

                }
                else if (pickUpScript.handObj.name == "Knife" && transform.Find("Flor Pet"))//������Ʒ�ǲ˵��Ұ������л����򽫻���ݻ٣����ۼ���
                {
                    Destroy(flower.gameObject);
                    if (pollen != null)
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
}
