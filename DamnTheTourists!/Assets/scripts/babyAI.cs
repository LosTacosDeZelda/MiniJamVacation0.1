/*
    Using a simple statemachine it chosses and goes to a random location.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class babyAI : EntityUtil
{

    // Start is called before the first frame update
    void Start()
    {
        entityStart();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: if else statemachine, between movingToPoint, findingNewPoint
        //TODO: MovingToPoint makes creature walk to point and once close it finds a new point
        //TODO: findingNewPoint finds point in arenas range
    }
}
