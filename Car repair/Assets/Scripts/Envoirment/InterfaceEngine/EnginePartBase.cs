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
    bool hasMoved;



    public void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    #region moveObject
    protected void MoveObject(float x, float y, float z)
    {
        isPlaying = true;
        Vector3 moveTowards;

        if (!hasMoved)
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
        hasMoved = !hasMoved;
        isPlaying = false;
    }
    #endregion
}
