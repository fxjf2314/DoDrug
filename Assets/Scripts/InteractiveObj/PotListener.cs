using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PotListener : Listener
{
    private PickUp pickUpScript;
    public TextMeshProUGUI tips;
    public Bag myBag;

    Transform pot;
    //水贴图
    Transform water;
    Transform pollen;
    Outline outline;
    bool isFinish;

    protected override void Start()
    {
        base.Start();
        //pollen = transform.Find("Pollen");
        pickUpScript = objWithInteractive.GetComponent<PickUp>();
        somethingScript = objWithInteractive.GetComponent<Interactive>();
        somethingScript.eventMakeTea += Dosomething;
        outline = transform.GetComponent<Outline>();
        isFinish = false;
    }

    protected override void Dosomething()
    {
        if (PickAndInteractiveFather.pickObj.name == transform.name)
        {
            //获取手中物体
            if (pickUpScript.handObj == null && !isFinish)//手中没有物品则弹出提示
            {
                tips.text = "Nothing can put on the hearth";
                tips.gameObject.SetActive(true);
            }
            else
            {
                if (isFinish)
                {
                    transform.tag = "Untagged";
                    outline.enabled = false;
                    transform.Find("Tea").gameObject.layer = LayerMask.NameToLayer("Default");
                    transform.Find("Tea").gameObject.SetActive(true);
                    transform.DetachChildren();
                }
                else if (pickUpScript.handObj.name == "Water")
                {
                    water = pickUpScript.handObj.transform;
                    myBag.items.Remove(water.GetComponent<ItemOnWorld>().thisItem);
                    BagManager.RemoveItemSlot(water.gameObject.GetComponent<ItemOnWorld>().thisItem);
                    //相应变量置空
                    pickUpScript.handObj = null;
                    GetAItem.inHandObj = null;
                    pickUpScript.handEmpty = true;
                    Destroy(water.gameObject);
                    transform.Find("water").gameObject.SetActive(true);
                }
                else if (pickUpScript.handObj.name == "Pollen" && transform.Find("water").gameObject.activeSelf)
                {
                    pollen = pickUpScript.handObj.transform;
                    myBag.items.Remove(pollen.GetComponent<ItemOnWorld>().thisItem);
                    BagManager.RemoveItemSlot(pollen.gameObject.GetComponent<ItemOnWorld>().thisItem);
                    //相应变量置空
                    pickUpScript.handObj = null;
                    GetAItem.inHandObj = null;
                    pickUpScript.handEmpty = true;
                    Destroy(pollen.gameObject);
                    transform.Find("water").gameObject.SetActive(false);
                    isFinish = true;
                }


                //tips.text = "This object should not be placed here";
                //tips.gameObject.SetActive(true);
            }

        }
    }
}
