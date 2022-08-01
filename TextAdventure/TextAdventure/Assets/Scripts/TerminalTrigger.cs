using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TerminalTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    private Interpreter interpreter;
    private Terminal terminal;

    public List<string> text= new List<string>();

    public UnityEvent enterEvent;

    private bool playerInRange;

    private void Awake()
    {
        interpreter = FindObjectOfType<Interpreter>();
        terminal = FindObjectOfType<Terminal>();

        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange)
        {
            visualCue.SetActive(true);

            if (!GameManager.Instance.terminalUI.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
            {
                TerminalOutput();
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
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
            GameManager.Instance.CloseTerminal();
        }
    }

    private void TerminalOutput()
    {
        terminal.ClearConsole();
        StartCoroutine(Timer());

        enterEvent.Invoke();
        GameManager.Instance.OpenTerminal();
    }

    IEnumerator Timer()
    {
        foreach(var t in text)
        {
            GameManager.Instance.terminalActive = true;
            terminal.AddDirectoryLine(t);
            yield return new WaitForSeconds(1.8f);
        }
        GameManager.Instance.terminalActive = false;
    }
}
