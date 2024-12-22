using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BagManager : MonoBehaviour
{
    static BagManager instance;

    public Bag myBag;
    public GameObject slotGrid;
    public Slot slot;
    public TextMeshProUGUI itemInformation;
    public List<Slot> grids=new List<Slot>();

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
    public static void RemoveItemSlot(Item itemToRemove)
    {
        foreach (Transform child in instance.slotGrid.transform)
        {
            Slot currentSlot = child.GetComponent<Slot>();
            if (currentSlot != null && currentSlot.slotItem == itemToRemove)
            {
                Destroy(child.gameObject);
                break; // 如果只有一个匹配项，找到后退出循环
            }
        }
    }
    public static void UpdateItemInfo(string itemDescription)
    {
        instance.itemInformation.text = itemDescription;
    }
    public static void CreateItem(Item item,Transform handObj)
    {
        Slot newItem = Instantiate(instance.slot, instance.slotGrid.transform.position,Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.itemImage;
        newItem.slotObj = handObj;
    }
}
