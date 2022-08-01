using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TextTrigger : MonoBehaviour
{
    private Terminal terminal;
    public UnityEvent enterEvent;
    public UnityEvent exitEvent;

    public List<string> text = new List<string>();

    private void Awake()
    {
        terminal = FindObjectOfType<Terminal>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.inRange = true;
            TerminalOutput();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.inRange = false;
            CloseTerminal();
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void CloseTerminal()
    {
        terminal.ClearConsole();
        GameManager.Instance.CloseTerminal();
    }

    private void TerminalOutput()
    {
        terminal.ClearConsole();
        enterEvent.Invoke();
        StartCoroutine(Timer());
        exitEvent.Invoke();
        GameManager.Instance.OpenTerminal();
    }

    IEnumerator Timer()
    {
        foreach (var t in text)
        {
            GameManager.Instance.terminalActive = true;
            terminal.AddDirectoryLine(t);
            yield return new WaitForSeconds(3f);
        }
        GameManager.Instance.terminalActive = false;
    }
}
