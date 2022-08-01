using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> container = new List<InventorySlot>();

    public void AddItem(ItemObject _item, int _amount)
    {
        bool hasItem = false;


        //loop through list and see if item already exists
        for (int i = 0; i < container.Count; i++)
        {
            if(container[i].item == _item)
            {
                container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }

        //
        if (!hasItem)
        {
            container.Add(new InventorySlot(_item, _amount));
        }

        GameManager.Instance.managerItems.Add(_item);
    }

    public void RemoveItem(ItemObject _item)
    {
        foreach(InventorySlot item in container)
        {
            if(item.item.itemName == "Key")
            {
                item.RemoveAmount(1);
            }
        }

        GameManager.Instance.managerItems.Remove(_item);
    }


    //remove item
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;
    public InventorySlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }

    public void RemoveAmount(int value)
    {
        amount -= value;
    }
}
