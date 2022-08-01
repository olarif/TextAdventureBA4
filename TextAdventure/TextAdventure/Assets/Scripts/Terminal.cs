using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Terminal : MonoBehaviour
{

    List<GameObject> lines = new List<GameObject>();

    public GameObject directoryLine;
    public GameObject responseLine;

    public TMP_InputField terminalInput;
    public GameObject userInputLine;
    public ScrollRect scrollrect;
    public GameObject messageList;

    Interpreter interpreter;

    private void Start()
    {
        interpreter = GetComponent<Interpreter>();
    }

    private void OnGUI()
    {
        if(terminalInput.isFocused && terminalInput.text != "" && Input.GetKeyDown(KeyCode.Return))
        {
            //store user input text
            string userInput = terminalInput.text;

            //clear input field
            ClearInputField();

            //Instantiate a gameobject with a directory prefix
            AddDirectoryLine(userInput);

            //Add the interpretation lines
            int lines = AddInterpreterLines(interpreter.Interpret(userInput.ToLower()));

            //scroll to bottom of scrollRect
            ScrollToBottom(lines);

            //move user input line to the end
            userInputLine.transform.SetAsLastSibling();

            //refocus input field
            terminalInput.ActivateInputField();
            terminalInput.Select();
        }
    }

    public void ClearConsole()
    {
        //delete each directory or response line 
        foreach(var x in lines)
        {
            Destroy(x);
        }

        messageList.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

        ScrollToTop();
        ClearInputField();
    }

    private void ScrollToTop()
    {
        scrollrect.normalizedPosition = new Vector2(0, 1);
    }

    private void ScrollToBottom(int lines)
    {
        if(lines > 4)
        {
            scrollrect.velocity = new Vector2(0, 450);
        } else
        {
            scrollrect.verticalNormalizedPosition = 0;
        }
    }

    public void AddDirectoryLine(string userInput)
    {
        //resizing command line container 
        Vector2 msgListSize = messageList.GetComponent<RectTransform>().sizeDelta;
        messageList.GetComponent<RectTransform>().sizeDelta = new Vector2(msgListSize.x, msgListSize.y + 35f);

        //instantiate directory
        GameObject msg = Instantiate(directoryLine, messageList.transform);

        lines.Add(msg);

        //set child index
        msg.transform.SetSiblingIndex(messageList.transform.childCount - 1);

        //set text
        msg.GetComponentsInChildren<TextMeshProUGUI>()[1].text = userInput;

        FindObjectOfType<AudioManager>().Play("Boop");

        userInputLine.transform.SetAsLastSibling();
    }

    private void ClearInputField()
    {
        terminalInput.text = "";
    }

    int AddInterpreterLines(List<string> interpretation)
    {
        for(int i = 0; i < interpretation.Count; i++)
        {
            //Instantiate response line
            GameObject res = Instantiate(responseLine, messageList.transform);

            lines.Add(res);

            //set it to the end of all messages
            res.transform.SetAsLastSibling();

            //get size of the message list and resize
            Vector2 listSize = messageList.GetComponent<RectTransform>().sizeDelta;
            messageList.GetComponent<RectTransform>().sizeDelta = new Vector2(listSize.x, listSize.y + 35f);

            //set the text of this response line to be whatever the interpreter string is
            res.GetComponentInChildren<TextMeshProUGUI>().text = interpretation[i];

            FindObjectOfType<AudioManager>().Play("Boop");
        }

        
        return interpretation.Count;
    }
}
