using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touristAI : EntityUtil
{
    public float chickCaptureDistance;
    public float chickCaptureTime;
    babyAI closestBaby;
    Vector3 spawnLocation;
    int stateSave = 0;
    string[] states = {"goingToChick", "leaving", "capturingChick"};
    babyAI[] babbies;
    float despawnDistance = 1f;
    float timePassed = 0;

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
            //refreshes the list of babbies
            babbies = FindObjectsOfType<babyAI>();
            if (babbies[0] != null)
            {
                updateGoingToChick();
            }
        }
        else if (states[stateSave] == "leaving")
        {
            updateLeaving();
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

        //if we are in the distance we can capture the chick, updates the capturing timer
        if ( Vector3.Distance(transform.position, closestBaby.transform.position) < chickCaptureDistance)
        {
            timePassed += Time.deltaTime;
            if (timePassed > chickCaptureTime)
            {
                closestBaby.chickenCapture();
                stateSave = 1;
            }
        }
        else
        {
            timePassed = 0;
        }
    }

    void updateLeaving()
    {
        //walks towards exit
        Vector2 location = new Vector2(spawnLocation.x, spawnLocation.y);
        Vector2 thisPosition = new Vector2(transform.position.x, transform.position.y);
        moveEntity(location - thisPosition);

        //if close to exit despawn character
        //if we are in the distance we can capture the chick, updates the capturing timer
        Vector3 locationV3 = new Vector3(spawnLocation.x, spawnLocation.y, transform.position.z);
        if ( Vector3.Distance(transform.position, locationV3) < despawnDistance)
        {
            Destroy(gameObject);
        }
    }

    //TODO: If he has the babby and is quacked at he drops the child
    
}
