using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    pickup,
    equipment,
    Default
}
public abstract class ItemObject: ScriptableObject
{
    public string itemName;
    public GameObject prefab;
    public ItemType type;
    [TextArea(15, 20)]
    public string description;
}