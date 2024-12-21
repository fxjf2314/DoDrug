using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    static BagManager instance;

    public Bag myBag;
    public GameObject slotGrid;
    public Slot slot;
    public TextMeshProUGUI itemInformation;

    void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    private void OnEnable()
    {
        instance.itemInformation.text = "";
    }

    public static void UpdateItemInfo(string itemDescription)
    {
        instance.itemInformation.text = itemDescription;
    }
    public static void CreateItem(Item item)
    {
        Slot newItem = Instantiate(instance.slot, instance.slotGrid.transform.position,Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.itemImage;
    }
}
