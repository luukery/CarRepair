using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonbine : EnginePartBase, IEngine
{
    public BobineStekker cable;

    public void Hover(RaycastHit hit)
    {

        ItemData.Instance.hoverOver = hit.transform.gameObject;

        if (ItemData.Instance.holdingItem == holdingRequirement && !isPlaying && cable.hasMoved)
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
    public void Interact()
    {
        MoveObject(0, 0.5f, 0);
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

        if (!hasMoved && !broken)
        {
            CarData.Instance.repairedParts++;
        }
    }
}
