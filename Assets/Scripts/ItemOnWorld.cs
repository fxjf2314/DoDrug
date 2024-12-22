using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public Item thisItem;
    public Bag mybag;

    void Start()
    {
        mybag.items.Clear();
    }
    public void AddNewItem()
    {
        if (!mybag.items.Contains(thisItem))
        {
            mybag.items.Add(thisItem);
            BagManager.CreateItem(thisItem, gameObject.transform);
        }
    }
}
