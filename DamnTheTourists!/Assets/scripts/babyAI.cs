/*
    Using a simple statemachine it chosses and goes to a random location.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class babyAI : EntityUtil
{
    public float newPointDistance;
    public float parentFollowDist;
    public Vector2 walkingAreaB1;
    public Vector2 walkingAreaB2;
    public float minTimeToFollow = 1f;
    public float maxTimeToFollow = 3f;
    Vector2 walkPoint;
    int stateSave = 0;
    float stateLengthT = 0;
    string[] states = {"uninitialised", "walkingToPoint", "followingParent"};
    PlayerCtrl player;

    // Start is called before the first frame update
    void Start()
    {
        entityStart();
        player = FindObjectsOfType<PlayerCtrl>()[0];
        turnOffset = 90;
        print(player);
    }

    // Update is called once per frame
    void Update()
    {
        //if else statemachine, between movingToPoint, findingNewPoint
        if (states[stateSave] == "uninitialised")
        {
            startWalkToPoint();
        }
        else if (states[stateSave] == "walkingToPoint")
        {
            updateWalkToPoint();
        }
        else if (states[stateSave] == "followingParent")
        {
            updateFollowParent();
        }

        if (states[stateSave] != "followingParent") 
        {
            checkToFollowParent();
        }
    }

    void startWalkToPoint()
    {   
        walkPoint = new Vector2(Random.Range(walkingAreaB1.x, walkingAreaB2.x), Random.Range(walkingAreaB1.y, walkingAreaB2.y));
        stateSave = 1;
    }

    void updateWalkToPoint()
    {
        Vector3 walkPointv3 = new Vector3(walkPoint.x, walkPoint.y, transform.position.z);
        Vector2 thisPosition = new Vector2(transform.position.x, transform.position.y);
        moveEntity(walkPoint - thisPosition);
        //if we are in the distance to find a new point it finds a new point
        if ( Vector3.Distance(transform.position, walkPointv3) < newPointDistance)
        {
            startWalkToPoint();
        }
    }

    void checkToFollowParent()
    {
        //if we are in the distance to find a new point it finds a new point
        Vector3 myTransformNormalized = new Vector3(transform.position.x, transform.position.y, player.transform.position.z);
        //print("Distance from parent:" + Vector3.Distance(myTransformNormalized, player.transform.position));
        if ( Vector3.Distance(myTransformNormalized, player.transform.position) < parentFollowDist)
        {
            stateLengthT = Random.Range(minTimeToFollow, minTimeToFollow);
            stateSave = 2;
            Invoke("disableFollowParent", stateLengthT);
        }
    }

    void disableFollowParent()
    {
        stateSave = 0;
    }

    void updateFollowParent()
    {
        Vector2 playerV2 = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 thisPosition = new Vector2(transform.position.x, transform.position.y);
        moveEntity(playerV2 - thisPosition);
    }
    
}
