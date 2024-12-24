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
    //案板上的花
    Transform flower;
    //案板上的花粉
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
        //获取手中物体
        handObj = pickUpScript.handObj;
        if(handObj == null)//手中没有物品则弹出提示
        {
            tips.text = "Nothing can put on the cutting board";
            tips.gameObject.SetActive(true);
        }
        else
        {
            if(handObj.name == "Flor Pet")//手中物品是花瓣则将花瓣放到案板上
            {
                flower = handObj.transform;
                //从包里移除对应物体 
                myBag.items.Remove(flower.GetComponent<ItemOnWorld>().thisItem);
                BagManager.RemoveItemSlot(flower.gameObject.GetComponent<ItemOnWorld>().thisItem);
                //相应变量置空
                handObj = null;
                GetAItem.inHandObj = null;
                pickUpScript.handEmpty = true;
                flower.SetParent(transform);
                //设置位置
                flower.localPosition = new Vector3(0, 0, 0.0007f);
                
            }
            else if(handObj.name == "Knife" && transform.Find("Flor Pet"))//手中物品是菜刀且案板上有花瓣则将花瓣摧毁，花粉激活
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
