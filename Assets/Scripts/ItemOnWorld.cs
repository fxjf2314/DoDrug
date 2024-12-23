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
    private void Update()
    {
        int playerLayerIndex = LayerMask.NameToLayer("Player");
        if (gameObject.layer == playerLayerIndex)
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
                rb.useGravity = false;
            }
        }
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
