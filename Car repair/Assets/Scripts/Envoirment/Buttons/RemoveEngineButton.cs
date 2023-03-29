using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveEngineButton : MonoBehaviour
{
    [SerializeField] HoverRotation hood;
    [SerializeField] Material selectMaterial;
    [SerializeField] GameObject engine;
    [SerializeField] Collider[] engineColliders;

    Vector3 startPos;
    Vector3 engineStartPos;

   Transform removedEngineLocation;

    bool hovering;
    bool hasBeenPressed;
    bool engineIsMoving;
    

    Renderer rend;
    Material baseMaterial;
    


    // Start is called before the first frame update
    void Start()
    {
        if (!hood)
            Debug.LogError("Hood rotation script not found");

        ItemData.Instance.engineInsideOfCar = true;
        SetEngineColliders(false);

        startPos = transform.position;
        engineStartPos = engine.transform.position;
        rend = GetComponent<Renderer>();
        baseMaterial = rend.material;
        removedEngineLocation = engine.transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(ItemData.Instance.hoverOver == this.gameObject)
        {
            hovering = true;
        }
        else
        {
            hovering = false;
        }


        if (Input.GetMouseButtonDown(0) && !hasBeenPressed && hovering )
        {
            StartCoroutine(PressedAnimation());
        }


        if(hovering && !hasBeenPressed)
        {
            SetMaterial(selectMaterial);
        }
        else
        {
            SetMaterial(baseMaterial);
        }
    }

    void SetMaterial(Material material)
    {
        if (rend.material != material) 
                rend.material = material;
    }

    void SetEngineColliders(bool set)
    {
        for (int i = 0; i < engineColliders.Length; i++)
        {
            engineColliders[i].enabled = set;
        }
    }
    
    IEnumerator PressedAnimation()
    {
        hasBeenPressed = true;        
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);

        StartCoroutine(MoveEngineAnimation());
        yield return new WaitForEndOfFrame();

        while (engineIsMoving)
        {
            yield return new WaitForEndOfFrame();
        }
       
        transform.position = startPos;
        hasBeenPressed = false;

    }

    IEnumerator MoveEngineAnimation()
    {
        engineIsMoving = true;
        Vector3 endlocation;

        if (ItemData.Instance.engineInsideOfCar)
        {
            endlocation = removedEngineLocation.position;
        }
        else
        {
            endlocation = engineStartPos;
            
        }

        ItemData.Instance.engineInsideOfCar = !ItemData.Instance.engineInsideOfCar;



        SetEngineColliders(false);
        while (Vector3.Distance(engine.transform.position, endlocation) > 0.001f)
        {
            yield return null;
            engine.transform.position = Vector3.Slerp(engine.transform.position, endlocation, 3f * Time.deltaTime);
        }

        engine.transform.position = endlocation;

        if(!ItemData.Instance.engineInsideOfCar)
            SetEngineColliders(true);


        

        engineIsMoving = false;
    }
}
