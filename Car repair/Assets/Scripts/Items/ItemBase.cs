using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : Hover
{
    public bool holdingItem;

    [HideInInspector] public Vector3 returnLocation;
    [HideInInspector] public Quaternion returnRotation;

    private void Awake()
    {
        returnLocation = transform.position;
        returnRotation = transform.rotation;
    }

    protected new void Update()
    {
        base.Update();

        if (holdingItem)
        {
            print("Holding");
            Holding();
        }
    }
    void Holding()
    {
        transform.position = ItemData.Instance.holdLocation.position;
        transform.rotation = ItemData.Instance.holdLocation.rotation;
    }
}
