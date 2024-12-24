using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KitchenListener : Listener
{
    private PickUp pickUpScript;
    private Transform handObj;
    public TextMeshProUGUI tips;
    public Bag myBag;
    
    Transform pot;
    //水贴图
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
        //获取手中物体
        handObj = pickUpScript.handObj;
        if (handObj == null)//手中没有物品则弹出提示
        {
            tips.text = "Nothing can put on the hearth";
            tips.gameObject.SetActive(true);
        }
        else
        {
            if (handObj.name == "Pot")
            {
                pot = handObj.transform;
                //从包里移除对应物体 
                myBag.items.Remove(pot.GetComponent<ItemOnWorld>().thisItem);
                BagManager.RemoveItemSlot(pot.gameObject.GetComponent<ItemOnWorld>().thisItem);
                //相应变量置空
                handObj = null;
                GetAItem.inHandObj = null;
                pickUpScript.handEmpty = true;
                pot.SetParent(transform);
                //设置位置
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
