using UnityEngine;

public class HelloWorldScript : MonoBehaviour
{
    public string stringToPrint = "Hello Mecha";

    private float timeSincePrint = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PrintHelloWorld("Start");
    }

    // Update is called once per frame
    void Update()
    {
        timeSincePrint += Time.deltaTime;

        if(timeSincePrint >= 3f)
        { 
            PrintHelloWorld(stringToPrint);

            timeSincePrint = 0f;
        }

        //The next line of code is the old way of referencing key presses, we will update next class! 11/25/25
        if(Input.GetKeyDown(KeyCode.P))
        {
            PrintHelloWorld("P Pressed");
        }
    }

    public void PrintHelloWorld(string toPrint)

    {
        Debug.Log(toPrint);
    }
}
