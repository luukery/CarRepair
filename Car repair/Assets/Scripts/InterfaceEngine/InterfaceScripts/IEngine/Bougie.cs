using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bougie : EnginePartBase, IEngine
{
    public Bonbine bonbine;
    public void Hover(RaycastHit hit)
    {
        ItemData.Instance.hoverOver = hit.transform.gameObject;

        if (ItemData.Instance.holdingItem == holdingRequirement && !isPlaying)
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
        HUDData i = HUDData.Instance;

        i.SetButtonPositions(this.gameObject, i.Feest, i.Feest2, i.Feest3);
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
