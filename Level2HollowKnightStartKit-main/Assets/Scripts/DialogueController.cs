using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueController : MonoBehaviour 
{
    [Tooltip("How far away talking is allowed to happen")]
    public float minTalkDistance;
    [Tooltip("The object to measure distance against. Usually the player")]
    public Transform target;
    [Tooltip("The actual text for the conversation that happens")]
    public List<DialogueLine> dialogueLines;
    [Tooltip("The UI element to show when talking")]
    public GameObject dialogueScreen;
    [Tooltip("The text to change into the talking strings")]
    public TMP_Text textToModify;
    [Tooltip("A sound to play on talk (requires audio source)")]
    public AudioClip talkSound;
    [Tooltip("If true, dialogue starts through trigger instead of distance check")]
    public bool useTriggerStart = false;
    [Tooltip("The UI image to show for the character portrait")]
    public Image chatheadImage;
    [Tooltip("The 'Press E to continue' text")]
    public TMP_Text continueText;
    //2-13-26 stop movement start
    [Header("Player Control")]
    public PlayerController playerController;
    //2-13-26 stop movement end

    //2-13-26 spawn item start
    [Header("Reward Settings")]
    public GameObject itemToSpawn;     // Prefab to spawn
    public Transform spawnPoint;       // Where to spawn it
    public bool spawnOnDialogueEnd;    // Toggle per NPC

    private bool hasSpawnedItem = false;  // Prevent duplicates
    //2-13-26 spawn item end
    //2-13-26 stop repeating dialogue start
    [Header("Dialogue Settings")]
    public bool canOnlyTalkOnce = false;

    private bool hasTalked = false;
    //2-13-26 stop repeating dialogue end


    //A private int to know how far through the conversation we are
    private int talkTextIndex;

    //A bool for whether or not we're currently talking
    private bool showingDialogue;

    //The audiosource component to play sound from
    private AudioSource myAudioSource;

    //A gizmo to show how far we can be away and still talk
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minTalkDistance);
    }

	// Use this for initialization
	void Start ()



    {

        // NOTHING IS HAPPENING I AM TRYING TO DEBUG HERE <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        
        //    Debug.Log("DialogueController STARTED on " + gameObject.name);
        


        //Set our current index to 0
        talkTextIndex = 0;
        //Set whether or not we're showing dialogue to false
        showingDialogue = false;

        //Get our audiosource
        myAudioSource = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update()
    {
        if (useTriggerStart)
            return;
        // existing Update code here...

        //NOTHING IS HAPPENING, I AM DEBUGGING HERE <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        //Debug.Log("Update is running!");
        //fixed?

        //If we have a target (the player)
        if (!target)
        {
            //If we are close enough...
            //debugging here!<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            Debug.LogWarning("DialogueController has NO TARGET assigned");
            return;
        }

        float dist = GetDirection().magnitude;
        //Debug.Log("Distance to player = " + dist);

        if (IsWithinDistance(minTalkDistance))
        {
            //Listen for input
            DetectInput();
        }
        else if (showingDialogue)
        {
            EndDialogue();
        }
    }

    // ==========================
    //  MANUAL UPDATE (TRIGGER)
    // ==========================
    public void ManualUpdate()
    {
        if (!showingDialogue)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (talkTextIndex < dialogueLines.Count - 1)
            {
                ProgressDialogue();
            }
            else
            {
                EndDialogue();
            }
        }
    }

    //Listen for input
    private void DetectInput()
    {
        //--------------------------------------------------------------------------<<<added debug log here<<<
        Debug.Log("DetectInput called. ShowingDialogue = " + showingDialogue);

        if (Input.anyKeyDown)
            Debug.Log("A key was pressed: " + Input.inputString);

        //If the E key is pressed...
        if( Input.GetKeyDown( KeyCode.E ) )
        {
            // Prevent talking again if limited to once
            if (canOnlyTalkOnce && hasTalked)
                return;

            if (!showingDialogue)
            {
                BeginDialogue();
            }
            else if (talkTextIndex < dialogueLines.Count - 1)
            {
                ProgressDialogue();
            }
            else
            {
                EndDialogue();
            }
        }
      
    }

    //When dialogue starts...
    public void BeginDialogue()
    {
        //attempt3start
        if (playerController != null)
        {
            playerController.canMove = false;

            Rigidbody2D rb = playerController.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        }
        //attempt3end

        //Set showign dialogue to true
        showingDialogue = true;

        //Set the index to 0
        talkTextIndex = 0;
        //Change the text to the right piece of dialogue
        textToModify.text = dialogueLines[talkTextIndex].text;
        if (chatheadImage && dialogueLines[talkTextIndex].chathead != null)
            chatheadImage.sprite = dialogueLines[talkTextIndex].chathead;
        // Show the "Press E to continue" text if assigned
        if (continueText != null)
            continueText.gameObject.SetActive(true);
        //debugging
        Debug.Log("Showing dialogue screen for line: " + talkTextIndex);
        //Show the screen
        dialogueScreen.SetActive(true);

        //If we have an audio source and a sound, play it
        if(myAudioSource && talkSound)
        {
            myAudioSource.PlayOneShot(talkSound);
        }
    }

    public void ProgressDialogue()
    {
        //When progressing dialogue, increment the index...
        talkTextIndex++;

        if (talkTextIndex < dialogueLines.Count)
        {
            textToModify.text = dialogueLines[talkTextIndex].text;
            if (chatheadImage && dialogueLines[talkTextIndex].chathead != null)
                chatheadImage.sprite = dialogueLines[talkTextIndex].chathead;

            // Show the "Press E to continue" text if assigned
            if (continueText != null)
                continueText.gameObject.SetActive(true);

            if (myAudioSource && talkSound)
                myAudioSource.PlayOneShot(talkSound);
        }
        else
        {
            EndDialogue();
        }
    }
    //we are deleting this i guess? dialogueLines now! idk, might break the sound but w/e

                    //Ans change the text to the right piece of dialogue
       // textToModify.text = dialogueLines[talkTextIndex];

                 //If we have an audio source and a sound, play it
     //   if (myAudioSource && talkSound)
     //   {
     //       myAudioSource.PlayOneShot(talkSound);
     //   }
    

    public void EndDialogue()
{
    //When ending dialogue, turn off the dialogue screen
    showingDialogue = false;
    dialogueScreen.SetActive(false);

        if (continueText != null)
        continueText.gameObject.SetActive(false);


        //2-13-26 stop repeat dialogue start
        if (canOnlyTalkOnce)
            hasTalked = true;
        //2-13-26 stop repeat dialogue end

        //attempt2
        if (playerController != null)
            playerController.canMove = true;
        //attempt2 end

        //2-13-26 spawn item start
        // Spawn item if enabled and not already spawned
        if (spawnOnDialogueEnd && itemToSpawn != null && !hasSpawnedItem)
        {
            if (spawnPoint != null)
            {
                Instantiate(itemToSpawn, spawnPoint.position, Quaternion.identity);
            }
            else
            {
                Instantiate(itemToSpawn, transform.position + Vector3.right, Quaternion.identity);
            }

            hasSpawnedItem = true;
        }
        //2-13-26 spawn item end
    }
    public virtual bool IsWithinDistance(float distance)
    {
        //Check if we're close enough to the target
        return (GetDirection().magnitude < distance);
    }

    public virtual Vector3 GetDirection()
    {
        return target.position - transform.position;
    }
}

