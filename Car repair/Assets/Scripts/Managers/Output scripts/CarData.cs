using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarData : CarManager<CarData>
{
   [HideInInspector] public int amountOfParts;
   [HideInInspector] public int repairedParts;

    public Canvas canvas;
    public TextMeshProUGUI winLoseText;

    public void CheckCar()
    {
        StartCoroutine(ActualCheckCar());
    }

    public void Start()
    {
        canvas.enabled = true;
        winLoseText.text = "Repair the right front wheel";
        StartCoroutine(tempCooldown());
    }

    IEnumerator tempCooldown()
    {
        yield return new WaitForSeconds(5);
        canvas.enabled = false;
    }

    IEnumerator ActualCheckCar()
    {
        yield return new WaitForEndOfFrame();

        canvas.enabled = true;
        if(amountOfParts > repairedParts)
        {
            winLoseText.text = "You didn't repair all the parts";
        }
        else
        {
            winLoseText.text = "You fixed the car";
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
