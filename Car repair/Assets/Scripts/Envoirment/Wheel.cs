using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Wheel : ItemBase
{
    Rigidbody rb;
   public bool onCar;
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    public void Interaction()
    {
        if (ItemData.Instance.holdingItem == ItemData.HoldingItem.DRILL)
        {
            
            if (transform.parent)
            {
                transform.parent = null;
                rb.isKinematic = false;
                rb.useGravity = true;
            }
        }
        else
        {
            print("Missing item");
        }
    }
}
