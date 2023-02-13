using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverRotation : MonoBehaviour
{
    public Vector3 maxRotation;
    public Vector3 minRotation;

    float speed;


    public bool canSelect;

    Quaternion startRotation;
    float duration = 1;
    bool scalingUp;
    
    
    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSelect && ItemData.Instance.hoverOver == this.gameObject)
        {
            Selectable();
        }
        else if(transform.rotation != startRotation)
        {
            ScaleToSize();
        }
    }

    void Selectable()
    {
        if (scalingUp)
        {
            
        }
        else
        {
        }

        if (transform.localRotation.x >= maxRotation.x - 0.15f || transform.localRotation.x <= minRotation.x + 0.15f)
        {
            scalingUp = !scalingUp;
        }
    }

    void ScaleToSize()
    {
        float a = startRotation.x - transform.localRotation.x;
        float b = transform.localRotation.x - startRotation.x;

        if(a < -0.005f || b > 0.005f)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, startRotation, 1f * Time.deltaTime);
            scalingUp = true;
        }
        else
        {
            transform.localRotation = startRotation;
        }
    }


}
