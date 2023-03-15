using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobineStekker : EnginePartBase, IEngine
{
    public void Update()
    {

        if (ItemData.Instance.hoverOver != this.gameObject)
        {
            outline.enabled = false;
        }
       
    }
    public void Hover(RaycastHit hit)
    {
        ItemData.Instance.hoverOver = hit.transform.gameObject;


        if (ItemData.Instance.holdingItem == ItemData.HoldingItem.EMPTY && !isPlaying)
        {
            canInteract = true;
            outline.enabled = true;
        }
        else
        {
            canInteract = false;
            outline.enabled = false;
        }

        if (Input.GetMouseButtonDown(0) && canInteract)
        {
            Interact();
        }
    }

    public void Interact()
    {
        if (!isPlaying)
        {
            MoveObject(0f, 0, -0.1f);
        }
    }

   

}
