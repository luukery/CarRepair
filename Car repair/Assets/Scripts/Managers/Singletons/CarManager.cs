using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager<T> : MonoBehaviour where T : Component
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<T>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (!instance)
        {
            instance = this as T;
        }
    }
}
