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
    int motorBlockButtonLayer;
    int enginePartLayer;

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
        motorBlockButtonLayer = LayerMask.GetMask("MotorBlockButton");
        enginePartLayer = LayerMask.GetMask("EnginePart");
    }

    // Update is called once per frame
    void Update()
    {
     

        if (hit.transform)
        {
          //  print(hit.transform.name);
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
        #region repairLayer
        else if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, repairLayer))
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
        #endregion
        #region motorblockButtonlayer
        else if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, motorBlockButtonLayer))
        {
            ItemData.Instance.hoverOver = hit.transform.gameObject;

        }
        #endregion
        #region enginePartLayer
        else if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, enginePartLayer))
        {
            if (!ItemData.Instance.engineInsideOfCar)       //checks if the engine is still in the car
            {
                CheckDoubleHover();

                IEngine ie = (IEngine)hit.transform.GetComponent(typeof(IEngine));

                if(ie != null)
                {
                    ie.Hover(hit);
                }
            }
        }
        #endregion

       
      
      
        if (!hit.transform)  //reset hover
        {
            ItemData.Instance.hoverOver = null;
        }


    }

    void CheckDoubleHover()     //if there are multiple items to scan on 1 GameObject, you can call this function to make sure the raycast keeps working,
    {
        if (hit.transform != null && ItemData.Instance.hoverOver != null)
        {
            if (hit.transform.gameObject != ItemData.Instance.hoverOver)
            {
                ItemData.Instance.hoverOver = null;
            }
        }
    }
    IEnumerator Cooldown(int time )
    {
        cooldown = true;
        yield return new WaitForSeconds(time);
        cooldown = false;
    }
}
