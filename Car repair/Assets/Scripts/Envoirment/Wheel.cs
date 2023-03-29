using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : ItemBase
{
   public bool onCar;
   public bool brokenTire;

    private void OnEnable()
    {
        CheckCarButton.CheckCarParts += CheckPart;
    }

    private void OnDisable()
    {
        CheckCarButton.CheckCarParts -= CheckPart;

    }

    public void CheckPart()
    {
        CarData.Instance.amountOfParts++;

        if(onCar && !brokenTire && !holdingItem)
        {
            CarData.Instance.repairedParts++;
        }
    }
}
