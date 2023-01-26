using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDData : HUDManager<HUDData>
{
    Canvas hud;
    bool objectiveOpen;
    GameObject objective;

    void Start()
    {
        hud = Object.FindObjectOfType<Canvas>();
        objective = hud.transform.GetChild(0).gameObject;
    }

    public void SwitchMenu()
    {
        bool currentState = objective.activeInHierarchy;
        currentState = !currentState;

        objective.SetActive(currentState);
    }
}
