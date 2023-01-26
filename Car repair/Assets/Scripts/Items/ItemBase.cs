using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : Hover
{
    public bool holdItem;
    protected new void Update()
    {
        base.Update();

        if (holdingItem)
        {
            Holding();
        }
    }
    void Holding()
    {
        transform.position = ItemData.Instance.holdLocation.position;
        transform.rotation = ItemData.Instance.holdLocation.rotation;
    }
}
