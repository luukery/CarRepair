using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobineStekker : EnginePartBase, IEngine
{
    public Bonbine bonbine;
    public void Interact()
    {
        MoveObject(0f, 0, -0.1f);
    }

    public void Hover(RaycastHit hit)
    {
        ItemData.Instance.hoverOver = hit.transform.gameObject;

        if (ItemData.Instance.holdingItem == holdingRequirement && !isPlaying && !bonbine.hasMoved)
        {
            canInteract = true;
            outline.enabled = true;
        }
        else
        {
            canInteract = false;
            outline.enabled = false;
        }

        if (canInteract && !isPlaying)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Interact();
            }
        }

    }

    public void OnEnable()
    {
        CheckCarButton.CheckCarParts += CheckPart;
    }

    public void OnDisable()
    {
        CheckCarButton.CheckCarParts -= CheckPart;
    }

    public void CheckPart()
    {
        CarData.Instance.amountOfParts++;

        if(!hasMoved && !broken)
        {
            CarData.Instance.repairedParts++;
        }
    }
}
