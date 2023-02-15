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

    bool cooldown;

  
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

            if (Input.GetMouseButtonDown(0) && ib.canSelect)
            {
                ib.PickupItem(hit);
            }
        }
        #endregion
        #region wheelLayer
        else if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, carLayer)) //checks the layer "Carparts"
        {
            ItemData ii = ItemData.Instance;

           
            ii.hoverOver = hit.transform.gameObject;
           

            switch (ii.hoverOver.gameObject.tag)
            {
                case "Wheel":

                    if (!cooldown)
                    {
                        if (ii.holdingActualItem != ii.hoverOver && ii.holdingItem == ItemData.HoldingItem.DRILL) //the wheel is on the car
                        {
                            ii.hoverOver.GetComponent<Wheel>().canSelect = true;

                            if (Input.GetMouseButtonDown(0))    //screw / unscrew the tire
                            {
                                StartCoroutine(Cooldown(2));
                                ii.hoverOver.GetComponent<Wheel>().onCar = !ii.hoverOver.GetComponent<Wheel>().onCar;     //set sounds to lock and unlock the tire
                                ii.hoverOver.GetComponent<ItemBase>().canSelect = !ii.hoverOver.GetComponent<ItemBase>().canSelect;
                            }
                        }
                        else if (!ii.hoverOver.GetComponent<Wheel>().onCar && ii.holdingItem == ItemData.HoldingItem.EMPTY) //the wheel is not on the car anymore and your hands are empty
                        {
                            ii.hoverOver.GetComponent<Wheel>().canSelect = true;

                            if (Input.GetMouseButtonDown(0))
                            {
                                ii.hoverOver.GetComponent<Wheel>().PickupItem(hit);                                
                            }
                        }
                        else //you can't do anything with it
                        {
                            ii.hoverOver.GetComponent<Wheel>().canSelect = false;
                        }
                    }
                  
                    break;

                case "Exterior":
                    ii.hoverOver.GetComponent<HoverRotation>().canSelect = true;

                    if (Input.GetMouseButtonDown(0))
                    {
                        ii.hoverOver.GetComponent<HoverRotation>().open = !ii.hoverOver.GetComponent<HoverRotation>().open;
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
                ItemData.Instance.hoverOver.GetComponent<HoverSize>().canSelect = true;
                if (Input.GetMouseButtonDown(0))
                {
                    ItemData.Instance.holdingActualItem.GetComponent<Wheel>().brokenTire = false;
                    ItemData.Instance.hoverOver.GetComponent<HoverSize>().canSelect = false;

                }
            }
        }

        if (!hit.transform)
        {
            ItemData.Instance.hoverOver = null;
        }
    }

    

    IEnumerator Cooldown(int time )
    {
        cooldown = true;
        yield return new WaitForSeconds(time);
        cooldown = false;
    }
}
