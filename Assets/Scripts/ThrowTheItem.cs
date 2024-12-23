using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTheItem : MonoBehaviour
{
    public Bag myBag;
    private PickUp myHandObj;
    public void buttonOnClicked()
    {
        if (GetAItem.inHandObj.gameObject != null)
        {
            myHandObj = GameObject.Find("Main Camera").GetComponent<PickUp>();
            GetAItem.inHandObj.SetParent(GameObject.Find("PickUp").transform);
            GetAItem.inHandObj.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetAItem.inHandObj.gameObject.GetComponent<Rigidbody>().useGravity = true;
            GetAItem.inHandObj.gameObject.layer = LayerMask.NameToLayer("Ground");
            GetAItem.inHandObj.gameObject.GetComponent<Rigidbody>().AddForce(GameObject.Find("Player").transform.position * 0.25f, ForceMode.Impulse);
            myBag.items.Remove(GetAItem.inHandObj.gameObject.GetComponent<ItemOnWorld>().thisItem);
            BagManager.RemoveItemSlot(GetAItem.inHandObj.gameObject.GetComponent<ItemOnWorld>().thisItem);
            GetAItem.inHandObj=null;
            myHandObj.handObj = null;
            myHandObj.handEmpty = true;
        }
    }
}
