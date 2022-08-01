using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;

    public void StartDialogue()
    {
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    }

    private void Update()
    {
        if (playerInRange)
        {
            visualCue.SetActive(true);

            if (!DialogueManager.GetInstance().isPlaying && Input.GetKeyDown(KeyCode.E))
            {

                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);

            }

            if(DialogueManager.GetInstance().isPlaying && Input.GetKeyDown(KeyCode.Space))
            {
                DialogueManager.GetInstance().ContinueStory();
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}