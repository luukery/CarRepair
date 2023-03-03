using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveEngineButton : MonoBehaviour
{
    [SerializeField] HoverRotation hood;
    [SerializeField] Material selectMaterial;
    Vector3 startPos;

    bool hovering;
    bool hasBeenPressed;
    

    Renderer rend;
    Material baseMaterial;
    

    // Start is called before the first frame update
    void Start()
    {
        if (!hood)
            Debug.LogError("Hood rotation script not found");

        startPos = transform.position;
        rend = GetComponent<Renderer>();
        baseMaterial = rend.material;

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
    
    IEnumerator PressedAnimation()
    {
        hasBeenPressed = true;        
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);

        yield return new WaitForSeconds(0.2f);

        transform.position = startPos;
        hasBeenPressed = false;

    }
}
