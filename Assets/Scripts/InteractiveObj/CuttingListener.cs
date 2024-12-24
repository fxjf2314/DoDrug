using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CuttingListener : Listener
{
    private PickUp pickUpScript;
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
        if (PickAndInteractiveFather.pickObj.name == transform.name)
        {
            if (pickUpScript.handObj == null)//手中没有物品则弹出提示
            {
                tips.text = "Nothing can put on the cutting board";
                tips.gameObject.SetActive(true);
            }
            else
            {
                if (pickUpScript.handObj.name == "Flor Pet")//手中物品是花瓣则将花瓣放到案板上
                {
                    flower = pickUpScript.handObj.transform;
                    //从包里移除对应物体 
                    myBag.items.Remove(flower.GetComponent<ItemOnWorld>().thisItem);
                    BagManager.RemoveItemSlot(flower.gameObject.GetComponent<ItemOnWorld>().thisItem);
                    //相应变量置空
                    pickUpScript.handObj = null;
                    GetAItem.inHandObj = null;
                    pickUpScript.handEmpty = true;
                    flower.SetParent(transform);
                    //设置位置
                    flower.localPosition = new Vector3(-1.120478e-05f, -0.0003f, 0.041f);
                    flower.localEulerAngles = new Vector3(98.47299f, -84.793f, -88.49298f);
                    flower.gameObject.layer = LayerMask.NameToLayer("Default");

                }
                else if (pickUpScript.handObj.name == "Knife" && transform.Find("Flor Pet"))//手中物品是菜刀且案板上有花瓣则将花瓣摧毁，花粉激活
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
