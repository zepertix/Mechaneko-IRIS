using UnityEngine;

public class DialogueTrigger2D : MonoBehaviour
{
    [Tooltip("Reference to the dialogue controller for this trigger")]
    public DialogueController dialogueController;

    private bool playerInside = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("OnTriggerEnter2D called with: " + other.name);

        if (!other.CompareTag("Player"))
            return;

        //Debug.Log("Player entered trigger");

        if (dialogueController != null)
        {
            Debug.Log("Calling BeginDialogue");
            dialogueController.BeginDialogue();
        }
        else
        {
            Debug.LogWarning("DialogueController reference is missing!");
        }

        playerInside = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        //Debug.Log("Player exited trigger");

        playerInside = false;

        if (dialogueController != null)
        {
            dialogueController.EndDialogue();
        }
    }

    private void Update()
    {
        // Allow advancing dialogue with E the same as normal
        if (playerInside && dialogueController != null)
        {
            dialogueController.ManualUpdate();
        }
    }
}