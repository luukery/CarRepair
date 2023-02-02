using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : Hover
{
    public bool holdingItem;

    [HideInInspector] public Vector3 returnLocation;
    [HideInInspector] public Quaternion returnRotation;

    protected new void Start()
    {
        base.Start();
        returnLocation = transform.position;
        returnRotation = transform.rotation;
    }


    protected new void Update()
    {
        base.Update();

        

        if (holdingItem)
        {
            Holding();
        }
        else
        {
            ReturnItem();
        }

    }
    void Holding()
    {
        transform.position = ItemData.Instance.holdLocation.position;
        transform.rotation = ItemData.Instance.holdLocation.rotation; 
    }

    void ReturnItem()
    {
        if (this.transform.gameObject == ItemData.Instance.holdingActualItem && Vector3.Distance(transform.position, returnLocation) < 1.5f)
        {
            ItemData.Instance.holdingActualItem = null;
            ItemData.Instance.holdingItem = ItemData.HoldingItem.EMPTY;

            transform.position = returnLocation;
            transform.rotation = returnRotation;
        }
    }

    public void PickupItem(RaycastHit hit)
    {
        if(ItemData.Instance.holdingItem == ItemData.HoldingItem.EMPTY)
        {
            string tag = hit.transform.tag;
            ItemData.Instance.hoverOver.GetComponent<ItemBase>().holdingItem = true;

            switch (tag)
            {
                case "Drill":
                    ItemData.Instance.holdingItem = ItemData.HoldingItem.DRILL;
                    ItemData.Instance.holdingActualItem = this.gameObject;
                    break;
            }
        }
    }

    
}
