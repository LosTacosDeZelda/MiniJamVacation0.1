/*
    This is a basic script used for a player in the top-down2d view. It's functionality includes:
    1) movement with camera following
    2) basic interaction script

    Methods/messages this script calls to other scripts are:
    * null

    Requirements:
    * Unities new input system.

    Users notes:
    * It is reccomended that other modules are put in place instead of editing this script.

    Sources:
    https://answers.unity.com/questions/650460/rotating-a-2d-sprite-to-face-a-target-on-a-single.html
    https://docs.unity3d.com/ScriptReference/Quaternion.Slerp.html 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCtrl : EntityUtil
{
    [Header("Controller variables")]
    public float quakScareDistance;
    public InputAction move;
    public InputAction interact;
    AudioSource audioSrc;
    public AudioClip quack;
    touristAI[] tourists;
    
    [Header("Animation Variables")]
    public float headMoveSpeed = -2f;
    public float headChangeTime = 1f;
    public Transform head;
    public float wingRotationTorque = -2f;
    public float wingsChangeTime = 1f;
    public Transform wing1;
    public Transform wing2;
    bool wingsOpen = false;
    bool isWalking = false;

    // Enabling input
    private void OnEnable()  { move.Enable();  interact.Enable();  }
    private void OnDisable() { move.Disable(); interact.Disable(); }

    // Start is called before the first frame update
    void Start()
    {
        entityStart();
        //sets up method
        interact.started += ctx => callInteract();
        //temporary ignore rayCast layer
        turnOffset = -90;
        gameObject.layer = 2;
        //cache audiosource component
        audioSrc = GetComponent<AudioSource>();
        tourists = FindObjectsOfType<touristAI>();
        Invoke("changeHeadMoveDirection", headChangeTime);
    }

    // Update is called once per frame
    void Update()
    {
        //Gets 2d vector for movement
        Vector2 moveInput = move.ReadValue<Vector2>();
        isWalking = moveInput == new Vector2(0, 0) ? false : true;
        moveEntity(moveInput);
        //animations
        moveHead();
        //moveWings();
    }

    // Used to interact when a specific button is pressed, calls "interact" method in other object.
    void callInteract()
    {
        //code to make the duck quack
        audioSrc.PlayOneShot(quack,0.25f);
        print("Quack");
        //checks area to see closest enemy
        tourists = FindObjectsOfType<touristAI>();
        //if there are enemies
        if (tourists[0] != null)
        {
            //finds all tourist in scare area and makes them leave
            for (int i = 0; i < tourists.Length; i++) 
            {
                float touristDistance = Vector3.Distance(transform.position, tourists[i].transform.position);
                if (touristDistance < quakScareDistance)
                {
                    tourists[i].scared();
                }
            }
        }
        //startMovingWings();
    }

    //MOVES HEAD
    void moveHead()
    {
        head.Translate((Vector3.up * Time.deltaTime) * headMoveSpeed, Space.Self);
    }

    void changeHeadMoveDirection()
    {
        headMoveSpeed = -headMoveSpeed;
        Invoke("changeHeadMoveDirection", headChangeTime);
    }

    
    /* MOVES WINGS
    //starts the wings moving by allowiing the wings to turn, signalls to start closing the wings after a few seconds.
    void startMovingWings()
    {
        wingsOpen = true;
        Invoke("closeWings", wingsChangeTime);
    }

    //function allows the wings to rotate.
    void moveWings()
    {
        if (wingsOpen)
        {
            wing1.Rotate(0, 0, wingRotationTorque, Space.Self);
            wing2.Rotate(0, 0, wingRotationTorque, Space.Self);
        }
    }

    //function makes the wings close back by reversing the torgue, before finnaly singalling to stop all movement in wings.
    void closeWings()
    {
        wingRotationTorque = -wingRotationTorque;
        Invoke("stopMovingWings", wingsChangeTime);
    }

    void stopMovingWings()
    {
        wingRotationTorque = -wingRotationTorque;
        wingsOpen = false;
    }
    */
}
