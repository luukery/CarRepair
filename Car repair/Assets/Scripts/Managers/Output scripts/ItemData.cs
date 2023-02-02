using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ItemManager<ItemData>
{
    public enum HoldingItem
    {
        EMPTY,
        DRILL,
        WHEEL
    }

    public GameObject hoverOver;
    public HoldingItem holdingItem;
    public GameObject holdingActualItem;
    public Transform holdLocation;
}
