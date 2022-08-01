using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pickup Object", menuName = "Inventory System/Items/Pickup")]
public class PickupObject : ItemObject
{
    public int restoreHealthValue;

    private void Awake()
    {
        type = ItemType.pickup;
    }
}
