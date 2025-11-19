using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    //When we touch this object...
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Just print a message
        Debug.Log("Won Game");
    }
}
