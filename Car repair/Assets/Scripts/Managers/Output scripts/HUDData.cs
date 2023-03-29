using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDData : HUDManager<HUDData>
{
    Vector2[] buttonPos;

    void Start()
    {

    }

    
    void SetButtonPositions(int amount)
    {
        if(amount < 2)
        {
            Debug.LogError("Not enough buttons");
            return;
        }
        else if(amount > 4)
        {
            Debug.LogError("Too many buttons");
            return;
        }

        switch (amount)     //coords are hard coded. If there is a way to to this in a beter way í'd like to know.
        {
            case 2:
                buttonPos[0] = new Vector2(-200, -350);
                buttonPos[1] = new Vector2(200, -350);
                break;

            case 3:
                buttonPos[0] = new Vector2(-200, -300);
                buttonPos[1] = new Vector2(200, -300);
                buttonPos[2] = new Vector2(0, -400);

                break;

            case 4:
                buttonPos[0] = new Vector2(-200, -300);
                buttonPos[1] = new Vector2(200, -300);
                buttonPos[2] = new Vector2(-200, -400);
                buttonPos[3] = new Vector2(200, -400);
                break;
        }

        for (int i = 0; i < amount; i++)
        {

        }
    }

   
}
