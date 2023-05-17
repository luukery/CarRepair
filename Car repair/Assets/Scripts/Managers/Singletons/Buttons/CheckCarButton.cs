  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckCarButton : MonoBehaviour
{
    public static event Action CheckCarParts;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CheckCarParts?.Invoke();
        }
    }
}
