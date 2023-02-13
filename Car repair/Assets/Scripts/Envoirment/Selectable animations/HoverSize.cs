using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script makes the object go bigger and smaller if you can select it
public class HoverSize : MonoBehaviour
{
    public float maxScale;
    public float minScale;
    public bool canSelect;

    Vector3 startingScale;
    Vector3 ImaxScale;
    Vector3 IminScale;

    float duration = 1f;

    bool scalingUp;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        startingScale = transform.localScale;
        ImaxScale = new Vector3(maxScale, maxScale, maxScale);
        IminScale = new Vector3(minScale, minScale, minScale);
    }

    protected void Update()
    {
        if (ItemData.Instance.hoverOver == this.gameObject && canSelect)
        {
            Selectable();
        }
        else if(transform.localScale != startingScale)
        {
            ScaleToSize();
        }  
    }

    void Selectable( )
    {
        if (scalingUp)
        {

        }
        else
        {
            //transform.localScale = Vector3.Lerp(transform.localScale, IminScale, duration * Time.deltaTime);
        }

        if (transform.localScale.x >= ImaxScale.x - 0.15f || transform.localScale.x <= IminScale.x + 0.15f)
        {
            scalingUp = !scalingUp;
        }
    }

    void ScaleToSize()
    {
        float a = startingScale.x - transform.localScale.x;
        float b = transform.localScale.x - startingScale.x;

        if (a < -0.005f || b > 0.005f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, startingScale, 1f * Time.deltaTime);
            scalingUp = true;
        }
        else
        {
            transform.localScale = startingScale;
        }
    }

   
}
