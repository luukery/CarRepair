using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
public class HUDData : HUDManager<HUDData>
{
    public GameObject basicButton;
    public GameObject canvas;
    public CharacterMovement characterMovement;

    List<Vector2> buttonPos = new List<Vector2>();
    List<Action> myFunctions = new List<Action>();
    List<GameObject> myButtons = new List<GameObject>();

    bool hasSpawend;

    void Update()
    {
      
        if (Input.GetMouseButtonDown(1))
        {
            RemoveButtons();
        }
    }


    public void SetButtonPositions(GameObject selectedObject, params Action[] functions)
    {
        if (hasSpawend)
            return;
        characterMovement.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        hasSpawend = true;
        canvas.SetActive(true);

        foreach (Action function in functions)
        {
            myFunctions.Add(function);
        }

        if (myFunctions.Count < 2)
        {
            Debug.LogError("Not enough buttons");
            RemoveButtons();
            return;
        }
        else if (myFunctions.Count > 4)
        {
            Debug.LogError("Too many buttons");
            RemoveButtons();
            return;
        }

        buttonPos.Clear();
        switch (myFunctions.Count)     //coords are hard coded. If there is a way to to this in a beter way í'd like to know.
        {
            case 2:

                buttonPos.Add(new Vector2(750, 175));
                buttonPos.Add(new Vector2(1150, 175));
                break;

            case 3:
                buttonPos.Add(new Vector2(750, 250));
                buttonPos.Add(new Vector2(1150, 250));
                buttonPos.Add(new Vector2(950, 150));

                break;

            case 4:
                buttonPos.Add(new Vector2(750, 250));
                buttonPos.Add(new Vector2(1150, 250));
                buttonPos.Add(new Vector2(750, 150));
                buttonPos.Add(new Vector2(1150, 150));
                break;
        }
 
        for (int i = 0; i < myFunctions.Count; i++)
        {
            GameObject b = Instantiate(basicButton);
            myButtons.Add(b);

            Action function = myFunctions[i];

            b.transform.SetParent(canvas.transform, true);
            b.transform.position = buttonPos[i];
            b.GetComponent<Button>().onClick.AddListener(() => function());


            b.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = function.Method.Name;
           
        }
    }

    public void RemoveButtons()
    {
        buttonPos.Clear();
        myFunctions.Clear();

        int q = myButtons.Count;

        for (int i = 0; i < q; i++)
        {
            Destroy(myButtons[0].gameObject);
            myButtons.RemoveAt(0);
        }
        canvas.SetActive(false);
        characterMovement.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        hasSpawend = false;
    }




    //button functions:
    public void Feest()
    {
        print("Dit werkt");
        RemoveButtons();
    }

    public void Feest2()
    {
        print("Dit werkt ook");
        RemoveButtons();

    }

    public void Feest3()
    {
        print("dit werkt erbij");
        RemoveButtons();

    }

    public void Feest4()
    {
        print("als dit werkt word het helemaal feest");
        RemoveButtons();

    }
}
