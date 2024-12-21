using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {diary,flashlight,clue};
[CreateAssetMenu(fileName ="New Item",menuName ="Bag/New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    [TextArea]
    public string itemText;
    public ItemType itemType;
}
