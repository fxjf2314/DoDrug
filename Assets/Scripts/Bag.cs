using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bag", menuName = "Bag/New Bag")]
public class Bag : ScriptableObject
{
    public List<Item> items=new List<Item>();
}
