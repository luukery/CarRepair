using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : HoverSize
{
    public bool holdingItem;
    bool canReturn;

    bool isAnimating;

    Vector3 returnLocation;
    Quaternion returnRotation;

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
            if (!Input.GetMouseButtonDown(0))
            {
                canReturn = true;
            }
            Holding();
        }

        if (this.transform.gameObject == ItemData.Instance.holdingActualItem && Vector3.Distance(transform.position, returnLocation) < 1.5f /*&&*/ /*!animIsPlaying*/)
        {
            if (canReturn && Input.GetMouseButtonDown(0))

                if(!isAnimating)
                    StartCoroutine(MovingAnimationReturn(this.gameObject, returnLocation, returnRotation));
        }



    }
    void Holding()
    {
        transform.position = ItemData.Instance.holdLocation.position;
        transform.rotation = ItemData.Instance.holdLocation.rotation;

        
    }

    

    public void PickupItem(RaycastHit hit)
    {
        if (!isAnimating)
        {
            if (ItemData.Instance.holdingItem == ItemData.HoldingItem.EMPTY)
            {
                #region set components
                ItemData ii = ItemData.Instance;

                ii.holdingActualItem = hit.transform.gameObject;

                if (GetComponent<Collider>() != null)
                {
                    GetComponent<Collider>().isTrigger = true;
                }



                #endregion



                string tag = hit.transform.tag;
                StartCoroutine(MovingAnimationPickup(this.gameObject, ItemData.Instance.holdLocation.transform.gameObject));


                switch (tag)
                {
                    case "Drill":
                        ItemData.Instance.holdingItem = ItemData.HoldingItem.DRILL;
                        break;

                    case "Wheel":
                        ItemData.Instance.holdingItem = ItemData.HoldingItem.WHEEL;

                        break;

                }
            }
        }
      
    }

    //basicly the same animation, but one can be used while moving
    IEnumerator MovingAnimationReturn(GameObject movingObject, Vector3 to, Quaternion rotation)
    {

        isAnimating = true;
        holdingItem = false;
        canReturn = false;
        ItemData.Instance.holdingActualItem = null;
        ItemData.Instance.holdingItem = ItemData.HoldingItem.EMPTY;

        float elapsedTime = 0;
        float waitTime = 0.5f;

        while (elapsedTime < waitTime)
        {
            transform.position = Vector3.Lerp(transform.position, to, elapsedTime / waitTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, elapsedTime / waitTime);
            elapsedTime += Time.deltaTime;


            yield return null;
        }

        isAnimating = false;
        yield return null;
    }




    IEnumerator MovingAnimationPickup(GameObject movingObject, GameObject to)
    {
        isAnimating = true;        
        float elapsedTime = 0;
        float waitTime = 0.5f;

        while (elapsedTime < waitTime)
        {
            Quaternion rotation = ItemData.Instance.holdLocation.transform.rotation;

            transform.position = Vector3.Lerp(transform.position, to.transform.position, elapsedTime / waitTime + 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, elapsedTime / waitTime);
            elapsedTime += Time.deltaTime;

            if (elapsedTime + 0.4f > waitTime)
            {
                break;
            }

            yield return null;
        }

        isAnimating = false;
        transform.position = to.transform.position;
        holdingItem = true;
        yield return null;
    }

   
    
}
