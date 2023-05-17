using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverRotation : MonoBehaviour
{
   public enum RotateAxis
    {
        X,
        Y,
        Z
    }

    public RotateAxis rotateAxis;
    public float previewRotation;
    public float openRotation;
    public bool canSelect;

    Quaternion startRotation;
    Quaternion IpreviewRotation;
    Quaternion IopenRotation;
  
    float speed = 5f;

   [HideInInspector] public bool open;
    
    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation;

        switch (rotateAxis)
        {
            case RotateAxis.X:
                IpreviewRotation.eulerAngles = new Vector3(previewRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                IopenRotation.eulerAngles = new Vector3(openRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                break;

            case RotateAxis.Y:
                IpreviewRotation.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, previewRotation, transform.rotation.eulerAngles.z);
                IopenRotation.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, openRotation, transform.rotation.eulerAngles.z);
                break;

            case RotateAxis.Z:
                IpreviewRotation.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, previewRotation);
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!open)
        {
            if (canSelect && ItemData.Instance.hoverOver == this.gameObject)
            {
                Selectable();
            }
            else if (transform.rotation != startRotation)
            {
                ScaleToSize();
            }
        }
        else
        {
            OpenItem();
        }
       
    }

    void Selectable()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, IpreviewRotation, speed * Time.deltaTime);

    }

    void ScaleToSize()
    {
        float a;
        float b;

        switch (rotateAxis)
        {
            case RotateAxis.X:
                a = startRotation.eulerAngles.x - transform.rotation.eulerAngles.x;
                b = transform.rotation.x - startRotation.x;
                break;

            case RotateAxis.Y:
                a = startRotation.eulerAngles.y - transform.rotation.eulerAngles.y;
                b = transform.rotation.eulerAngles.y - startRotation.eulerAngles.y;
                break;

            case RotateAxis.Z:
                 a = startRotation.eulerAngles.z - transform.rotation.eulerAngles.z;
                b = transform.rotation.z - startRotation.z;
                break;

            default:
                a = 0;
                b = 0;
                break;

        }

        if(a < -0.005f || b > 0.005f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, startRotation, speed * Time.deltaTime);
        }
        else
        {
            transform.localRotation = startRotation;
        }
    }

    void OpenItem()
    {
        if(transform.rotation != IopenRotation)
             transform.rotation = Quaternion.Lerp(transform.rotation, IopenRotation, speed * Time.deltaTime);

    }

}
