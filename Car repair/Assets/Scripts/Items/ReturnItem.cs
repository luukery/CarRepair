using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnItem : MonoBehaviour
{
    public bool canReturn;
    public ItemData.HoldingItem canBeReturnedHere;
    

    // Update is called once per frame
    void Update()
    {
        if (canReturn && canBeReturnedHere == ItemData.Instance.holdingItem)
        {
           
            if (Input.GetMouseButtonDown(0) && ItemData.Instance.holdingActualItem) 
            {
                ItemData.Instance.holdingActualItem.GetComponent<ItemBase>().holdingItem = false;
                //print("Holding item = " + ItemData.Instance.holdingActualItem.GetComponent<ItemBase>().holdingItem);

                ItemData.Instance.holdingActualItem.transform.position = ItemData.Instance.holdingActualItem.GetComponent<ItemBase>().returnLocation;
                ///print("position = " + ItemData.Instance.holdingActualItem.transform.position);
                
                ItemData.Instance.holdingActualItem.transform.rotation = ItemData.Instance.holdingActualItem.GetComponent<ItemBase>().returnRotation;
                //print("Rotation = " + ItemData.Instance.holdingActualItem.transform.rotation);

                ItemData.Instance.holdingActualItem = null;
                //print("holdingActualitem = " + ItemData.Instance.holdingActualItem);
                
                ItemData.Instance.holdingItem = ItemData.HoldingItem.EMPTY;
                //print("holdingItem = " + ItemData.Instance.holdingItem);
               
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ItemData.Instance.holdingItem != ItemData.HoldingItem.EMPTY)
        {
            canReturn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canReturn = false;
    }
}
