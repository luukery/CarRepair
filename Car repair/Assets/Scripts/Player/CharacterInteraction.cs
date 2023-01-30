using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    Camera cam;
    float range;

    int itemLayer;
    int carLayer;
    int repairLayer;

    RaycastHit hit;

  
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        range = 1.5f;

        itemLayer = LayerMask.GetMask("Item");
        carLayer = LayerMask.GetMask("CarPart");
        repairLayer = LayerMask.GetMask("Repair");
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
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, itemLayer))  //checks the layer "Items"
        {
            ItemBase ib = hit.transform.GetComponent<ItemBase>();
            


            ItemData.Instance.hoverOver = ib.gameObject;

            if (Input.GetMouseButtonDown(0) && ItemData.Instance.hoverOver.GetComponent<ItemBase>().canSelect)
            {
                PickupItem();
            }
        }
        #endregion
        #region wheelLayer
        else if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, carLayer)) //checks the layer "Carparts"
        {
            ItemData.Instance.hoverOver = hit.transform.gameObject;

            switch (ItemData.Instance.hoverOver.gameObject.tag)
            {
                case "Wheel":

                    

                    if (ItemData.Instance.holdingItem == ItemData.HoldingItem.DRILL && ItemData.Instance.hoverOver.GetComponent<Wheel>().onCar)  //you're holding the drill and you can remove the wheel from the car
                    {
                        ItemData.Instance.hoverOver.GetComponent<Wheel>().canSelect = true;
                        print("HIT");

                        if (Input.GetMouseButtonDown(0))
                        {
                            ItemData.Instance.hoverOver.GetComponent<Wheel>().onCar = false;
                         //   ItemData.Instance.hoverOver.GetComponent<Rigidbody>().isKinematic = false;
                           // ItemData.Instance.hoverOver.GetComponent<Rigidbody>().useGravity = true;
                            ItemData.Instance.hoverOver.GetComponent<Wheel>().canSelect = false;
                        }
                    }

                    if (!ItemData.Instance.hoverOver.GetComponent<Wheel>().onCar && ItemData.Instance.holdingItem == ItemData.HoldingItem.EMPTY ) //the wheel is not on the car anymore and your hands are empty
                    {
                        ItemData.Instance.hoverOver.GetComponent<Wheel>().canSelect = true;

                        if (Input.GetMouseButtonDown(0))
                        {
                            ItemData.Instance.hoverOver.transform.GetComponent<Wheel>().holdingItem = true;
                            ItemData.Instance.hoverOver.GetComponent<Collider>().isTrigger = true;
                            ItemData.Instance.holdingActualItem = ItemData.Instance.hoverOver;
                            ItemData.Instance.holdingItem = ItemData.HoldingItem.WHEEL;


                        }

                    }

                    break;
            }
        }
        #endregion
        else if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, repairLayer))
        {
            ItemData.Instance.hoverOver = hit.transform.gameObject;
            if(ItemData.Instance.holdingItem == ItemData.HoldingItem.WHEEL && ItemData.Instance.holdingActualItem.GetComponent<Wheel>().brokenTire)
            {
                print("Pick up new tire");
                ItemData.Instance.hoverOver.GetComponent<Hover>().canSelect = true;
                if (Input.GetMouseButtonDown(0))
                {
                    ItemData.Instance.holdingActualItem.GetComponent<Wheel>().brokenTire = false;
                    ItemData.Instance.hoverOver.GetComponent<Hover>().canSelect = false;

                }
            }
        }





        if (!hit.transform)
        {
            ItemData.Instance.hoverOver = null;
        }

    }

    void PickupItem()
    {
        if(ItemData.Instance.holdingItem == ItemData.HoldingItem.EMPTY)
        {
            string tag = hit.transform.tag;
            ItemData.Instance.hoverOver.GetComponent<ItemBase>().holdingItem = true;
             switch (tag)    //set the instance to what you're holding
            {
                case "Drill":
                    ItemData.Instance.holdingItem = ItemData.HoldingItem.DRILL;
                    ItemData.Instance.holdingActualItem = ItemData.Instance.hoverOver.gameObject;
                    break;
            }
        }
        else
        {
            print("You have your hands full");
        }
       
    }
}
