

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    Vector3 startingScale;
    public Vector3 maxScale;
    public Vector3 minScale;

    float duration = 1f;
    bool scalingUp;

    //public bool hoveringOver;
    public bool canSelect;
    

    // Start is called before the first frame update
    protected virtual void Start()
    {
        startingScale = transform.localScale;
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

    public void Selectable( )
    {
            if (scalingUp)
            {
               transform.localScale = Vector3.Lerp(transform.localScale, maxScale, duration * Time.deltaTime);
            }
            else
            {
               transform.localScale = Vector3.Lerp(transform.localScale, minScale, duration * Time.deltaTime);
            }

            if (transform.localScale.x >= maxScale.x - 0.15f || transform.localScale.x <= minScale.x + 0.15f)
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
            transform.localScale = Vector3.Lerp(transform.localScale, startingScale, 10f * Time.deltaTime);
            scalingUp = true;
        }
        else
        {
            transform.localScale = startingScale;
        }
    }

   
}
