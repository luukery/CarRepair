using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarData : CarManager<CarData>
{
   [HideInInspector] public int amountOfParts;
   [HideInInspector] public int repairedParts;

    public void CheckCar()
    {
        StartCoroutine(ActualCheckCar());
    }

    IEnumerator ActualCheckCar()
    {
        yield return new WaitForEndOfFrame();

        if(amountOfParts > repairedParts)
        {
            print("You didn't repair all the parts");
        }
        else
        {
            print("You fixed the car");
        }

        
        amountOfParts = 0;
        repairedParts = 0;
    }

    private void OnEnable()
    {
        CheckCarButton.CheckCarParts += CheckCar;
    }

    public void OnDisable()
    {
        CheckCarButton.CheckCarParts -= CheckCar;
    }

}
