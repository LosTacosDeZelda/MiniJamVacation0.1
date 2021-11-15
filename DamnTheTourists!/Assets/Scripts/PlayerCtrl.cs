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
    public InputAction move;
    public InputAction interact;
    AudioSource audioSrc;
    public AudioClip quack;

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
    }

    // Update is called once per frame
    void Update()
    {
        //Gets 2d vector for movement
        Vector2 moveInput = move.ReadValue<Vector2>();
        moveEntity(moveInput);
    }

    // Used to interact when a specific button is pressed, calls "interact" method in other object.
    void callInteract()
    {
        //code to make the duck quack
        audioSrc.PlayOneShot(quack,0.25f);
        print("Quack");
        //checks area to see closest enemy
        
    }
}
