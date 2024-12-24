using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KitchenListener : Listener
{
    private PickUp pickUpScript;
    public TextMeshProUGUI tips;
    public Bag myBag;
    
    Transform pot;
    //ˮ��ͼ
    Transform water;
    Transform pollen;
    Outline outline;

    protected override void Start()
    {
        base.Start();
        //pollen = transform.Find("Pollen");
        pickUpScript = objWithInteractive.GetComponent<PickUp>();
        somethingScript = objWithInteractive.GetComponent<Interactive>();
        somethingScript.eventPutPot += Dosomething;
        outline = transform.GetComponent<Outline>();
    }

    protected override void Dosomething()
    {
        if (PickAndInteractiveFather.pickObj.name == transform.name)
        {
            //��ȡ��������
            if (pickUpScript.handObj == null)//����û����Ʒ�򵯳���ʾ
            {
                tips.text = "Nothing can put on the hearth";
                tips.gameObject.SetActive(true);
            }
            else
            {
                if (pickUpScript.handObj.name == "Pot")
                {
                    pot = pickUpScript.handObj.transform;
                    //�Ӱ����Ƴ���Ӧ���� 
                    myBag.items.Remove(pot.GetComponent<ItemOnWorld>().thisItem);
                    BagManager.RemoveItemSlot(pot.gameObject.GetComponent<ItemOnWorld>().thisItem);
                    //��Ӧ�����ÿ�
                    pickUpScript.handObj = null;
                    GetAItem.inHandObj = null;
                    pickUpScript.handEmpty = true;
                    pot.SetParent(transform);
                    //����λ��
                    pot.localPosition = new Vector3(0.00565f, 0.0038f, 0.01281f);
                    pot.localEulerAngles = new Vector3(90, 0, 0);
                    pot.tag = "InteractiveObj";
                    PotListener potListenerScript;
                    if (potListenerScript = pot.GetComponent<PotListener>())
                    {
                        potListenerScript.enabled = true;
                        pot.gameObject.layer = LayerMask.NameToLayer("Default");
                    }
                    transform.tag = "Untagged";
                    outline.enabled = false;
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
