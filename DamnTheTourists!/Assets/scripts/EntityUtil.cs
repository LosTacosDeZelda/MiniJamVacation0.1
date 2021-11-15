/*
    All entities inherit from this class it provides basic functionaity held across entities.
    TODO: write rest of documentation
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class EntityUtil : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 5;
    public float strengthOfRot = 100;
    public float interactionDistance = 2;
    protected float turnOffset = 0;
    Vector2 lastFacedDirection = new Vector2(0, 0);

    // Start is called before the first frame update
    protected void entityStart()
    {
        //sets up users physics automatically
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        rb.gravityScale = 0;
        //standardises location
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        //Ignores collisions between layers 0 and 8, as this game doesn't have collisions
        Physics2D.IgnoreLayerCollision(0, 8);
    }

    protected void moveEntity(Vector2 location)
    {
        location.Normalize();
        Vector3 locationv3 = new Vector3(location.x, location.y, 0);
        //Moves the player by directly altering velocity. 
        //Avoids acceleration issues (since not using force), avoids rb2d issues (since not using positions).
        rb.velocity = locationv3 * speed;

        //Rotates creature and keeps it facing in the last faced direction if they didn't move.
        if (location != new Vector2(0,0)) 
        { 
            rotateEntity(locationv3); 
        } 

        //Saves last faced direction
        lastFacedDirection = location;
    }

    /* MOVEMENT UTILITIES */

    protected void rotateEntity(Vector3 direction)
    {
        //Rotation speed is dependant on strength
        direction = direction * strengthOfRot;
        //Gets vector between 2 poins
        Vector3 vectorToTarget = direction - transform.position;
        //formula to calculate bearing between 2 angles
        float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) + turnOffset;
        //Puts this angle into unity coordinates, specifically on the z acis, as dontated by Vector3.forward
        Quaternion quaternionDirection = Quaternion.AngleAxis(angle, Vector3.forward);
        //Sends angle to unity
        transform.rotation = Quaternion.Slerp(transform.rotation, quaternionDirection, 0.1f);
    }
}
