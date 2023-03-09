using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    [System.Serializable]
    public struct Part
    {
        public string name;
        public GameObject partObject;
        public Outline outline;
        public bool broken;

    }

    [SerializeField] public Part[] parts;
    void Start()
    {
        for (int i = 0; i < parts.Length; i++)
        {
            if(parts[i].name == null)
            {
                Debug.LogError("There is no name for struct " + parts[i].name);
            }

            if(parts[i].partObject == null)
            {
                Debug.LogError("There is GameObject selected for " + parts[i].name);
            }

            parts[i].outline = parts[i].partObject.GetComponent<Outline>();

            if (parts[i].partObject.GetComponent<Outline>() != parts[i].outline)
            {
                Debug.LogError("The right Outline script is not selected for " + parts[i].name);
            }
        }
    }

   
}
