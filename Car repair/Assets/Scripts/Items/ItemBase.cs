using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : Hover
{
    public bool holdingItem;
    PutItemOnCar returnScript;

    [HideInInspector] public Vector3 returnLocation;
    [HideInInspector] public Quaternion returnRotation;

    private void Awake()
    {
        returnScript = GetComponent<PutItemOnCar>();
        returnLocation = transform.position;
        returnRotation = transform.rotation;
    }

    protected new void Update()
    {
        base.Update();

        if (holdingItem)
        {
            returnScript.enabled = true;
            print("Holding");
            Holding();
        }
        else
        {
            returnScript.enabled = false;
        }
     
    }
    void Holding()
    {
        transform.position = ItemData.Instance.holdLocation.position;
        transform.rotation = ItemData.Instance.holdLocation.rotation;
    }
}
