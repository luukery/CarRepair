using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bougie : EnginePartBase, IEngine
{

    public void Hover(RaycastHit hit)
    {

    }

    public void Interact()
    {

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
