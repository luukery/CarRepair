using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutItemOnCar : MonoBehaviour
{
    Vector3 returnLocation;
    Quaternion returnRotation;

    // Start is called before the first frame update
    void Start()
    {
        returnLocation = transform.position;
        returnRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.gameObject == ItemData.Instance.holdingActualItem && Vector3.Distance(transform.position, returnLocation) < 1.5f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ItemData.Instance.holdingActualItem.GetComponent<ItemBase>().holdingItem = false;

                ItemData.Instance.holdingActualItem = null;
                ItemData.Instance.holdingItem = ItemData.HoldingItem.EMPTY;

                transform.position = returnLocation;
                transform.rotation = returnRotation;
            }
        }
    }
}
