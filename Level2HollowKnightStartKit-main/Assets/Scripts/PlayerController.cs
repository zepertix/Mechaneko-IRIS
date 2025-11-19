using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script NEEDS a mover and a jumper to work. This will automatically add them if there isn't one of each
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Jumper))]

public class PlayerController: MonoBehaviour
{
    //these are all just references to the various components attached to this object to make our lives easier. We'll add more as we go!
    private Mover mover;
    private Jumper jumper;

    void Start()
    {
        //Find all the componenets attached to this object and save them to references
        mover = gameObject.GetComponent<Mover>();
        jumper = gameObject.GetComponent<Jumper>();
    }

    // Update is called once per frame
    void Update()
    {
        //Moving Right
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            //When right key is pressed, accelerate towards the right...
            mover.AccelerateInDirection(new Vector2(1f, 0f));

            //And flip our entire body to face the right
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
        }

        //Moving Left
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            //When left key is pressed, accelerate towards the left...
            mover.AccelerateInDirection(new Vector2(-1f, 0f));

            //And flip our entire body to face the left
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);
        }

        //When Jumping
        if ( Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) )
        {
            //If the jump key is pressed... jump!
            jumper.Jump();
        }
    }
}
