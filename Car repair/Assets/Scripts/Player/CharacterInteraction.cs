using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    Camera cam;
    float range;
    int itemLayer;
    int carLayer;

    RaycastHit hit;

  
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        range = 1;

        itemLayer = LayerMask.GetMask("Item");
        carLayer = LayerMask.GetMask("CarPart");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            HUDData.Instance.SwitchMenu();
        }
        Raycast();
      
    }    

    void Raycast()
    {
        

        #region ItemLayer
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, itemLayer))  //checks if there is an item that the player can hold on the layer "item"
        {
            ItemBase ib = hit.transform.GetComponent<ItemBase>();
            ib.hoveringOver = true;
            ItemData.Instance.hoverOver = ib.gameObject;

            if (Input.GetMouseButtonDown(0))
            {
                PickupItem();
            }
        }
        else if (ItemData.Instance.hoverOver && ItemData.Instance.hoverOver.gameObject.layer == itemLayer)
        {
            ItemData.Instance.hoverOver.GetComponent<ItemBase>().hoveringOver = false;
            ItemData.Instance.hoverOver.GetComponent<ItemBase>().shrinkToSize = true;
            ItemData.Instance.hoverOver = null;
        }
        #endregion

        #region Car layer

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range + 5, carLayer)) //checks if there is an item from the car that the player can interact with
        {
            ItemData.Instance.hoverOver = hit.transform.gameObject;

            switch (ItemData.Instance.hoverOver.gameObject.tag)
            {
                case "Wheel":

                    bool c = ItemData.Instance.hoverOver.GetComponent<Wheel>().onCar;

                    if(ItemData.Instance.holdingItem == ItemData.HoldingItem.DRILL && c)  //you're holding the drill and you can remove the wheel from the car
                    {
                        ItemData.Instance.hoverOver.GetComponent<Wheel>().hoveringOver = true;

                        if (Input.GetMouseButtonDown(0))
                        {
                            ItemData.Instance.hoverOver.GetComponent<Rigidbody>().isKinematic = false;
                            ItemData.Instance.hoverOver.GetComponent<Rigidbody>().useGravity = true;
                            ItemData.Instance.hoverOver.GetComponent<Wheel>().onCar = false;
                        }
                    }

                    if(ItemData.Instance.holdingItem == ItemData.HoldingItem.EMPTY && !ItemData.Instance.hoverOver.GetComponent<Wheel>().onCar) //the wheel is not on the car anymore and your hands are empty
                    {

                    }

                    break;
            }
        }
        #endregion


        if (!hit.transform) //reset
        {

            ItemData.Instance.hoverOver = null;
        }
    }

    void PickupItem()
    {
        string tag = hit.transform.tag;
        ItemData.Instance.hoverOver.GetComponent<ItemBase>().holdingItem = true;
        ItemData.Instance.hoverOver.GetComponent<ItemBase>().DisableScaling();
        
        switch (tag)    //set the instance to what you're holding
        {
            case "Drill":
                ItemData.Instance.holdingItem = ItemData.HoldingItem.DRILL;
                ItemData.Instance.holdingActualItem = ItemData.Instance.hoverOver.gameObject; 
                break;
        }
    }
}
