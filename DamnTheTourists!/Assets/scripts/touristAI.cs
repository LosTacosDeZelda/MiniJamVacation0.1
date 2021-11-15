using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touristAI : EntityUtil
{
    public float chickCaptureDistance;
    babyAI closestBaby;
    Vector3 spawnLocation;
    int stateSave = 0;
    string[] states = {"goingToChick", "leaving", "capturingChick"};
    babyAI[] babbies;

    // Start is called before the first frame update
    void Start()
    {
        entityStart();
        spawnLocation = transform.position;
        turnOffset = 90;
        babbies = FindObjectsOfType<babyAI>();
        closestBaby =  babbies[0];
    }

    // Update is called once per frame
    void Update()
    {
        //if else statemachine, between movingToPoint, findingNewPoint
        if (states[stateSave] == "goingToChick")
        {
            updateGoingToChick();
        }
        else if (states[stateSave] == "leaving")
        {
            
        }
        else if (states[stateSave] == "capturingChick")
        {
            
        }
    }

    void updateGoingToChick()
    {
        //finds closest chick
        float closestBabyDistance = Vector3.Distance(transform.position, closestBaby.transform.position);
        for (int i = 0; i < babbies.Length; i++) 
        {
            float babyDistance = Vector3.Distance(transform.position, babbies[i].transform.position);
            if (babyDistance < closestBabyDistance)
            {
                closestBaby = babbies[i];
                closestBabyDistance = babyDistance;
            }
        }

        //walks towards chick
        Vector2 closestBabyPos = new Vector2(closestBaby.transform.position.x, closestBaby.transform.position.y);
        Vector2 thisPosition = new Vector2(transform.position.x, transform.position.y);
        moveEntity(closestBabyPos - thisPosition);

        //if we are in the distance we can capture the chick
        if ( Vector3.Distance(transform.position, closestBaby.transform.position) < chickCaptureDistance)
        {
            stateSave = 2;
        }
    }

}
