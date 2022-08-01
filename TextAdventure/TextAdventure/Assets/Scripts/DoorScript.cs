using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject doorOpen;
    public GameObject doorClosed;

    public ItemObject key;

    private Terminal terminal;

    public List<string> text = new List<string>();

    private void Awake()
    {
        terminal = FindObjectOfType<Terminal>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.door = this;
            GameManager.Instance.inRange = true;
            TerminalOutput();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.door = null;
            GameManager.Instance.inRange = false;
            CloseTerminal();
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

        foreach (var x in text)
        {
            terminal.AddDirectoryLine(x);
        }

        GameManager.Instance.OpenTerminal();
        //terminal.userInputLine.transform.SetAsLastSibling();
    }

    public void OpenDoor()
    {
        FindObjectOfType<AudioManager>().Play("DoorOpen");
        doorClosed.SetActive(false);
        doorOpen.SetActive(true);
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void CloseDoor()
    {
        doorOpen.SetActive(false);
        doorClosed.SetActive(true);
    }
}
