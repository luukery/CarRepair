using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class EnginePartBase : MonoBehaviour
{
    protected bool canInteract;
    protected bool broken;

    protected bool isPlaying;

    protected Outline outline;

    Vector3 startPos;
   [HideInInspector] public bool hasMoved;

   [SerializeField] protected ItemData.HoldingItem holdingRequirement;


    public void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public void Update()
    {
        if (ItemData.Instance.hoverOver != this.gameObject)
        {
            outline.enabled = false;
        }
    }

   

   

    #region moveObject
    protected void MoveObject(float x, float y, float z)
    {
        isPlaying = true;

        bool m = hasMoved;
        hasMoved = !hasMoved;


        Vector3 moveTowards;

        if (!m)
        {
            startPos = transform.position;

            float mX = transform.position.x + x;
            float mY = transform.position.y + y;
            float mZ = transform.position.z + z;

            moveTowards = new Vector3(mX, mY, mZ);
        }
        else
        {
            moveTowards = startPos;
        }

        StartCoroutine(moveTowrds(moveTowards));
        
    }

    IEnumerator moveTowrds(Vector3 moveTwrds)
    {
        yield return null;


        while (transform.position != moveTwrds)
        {
            yield return null;

            transform.position = Vector3.MoveTowards(transform.position, moveTwrds, 1 * Time.deltaTime);
        }

        transform.position = moveTwrds;
        isPlaying = false;
    }
    #endregion
}
