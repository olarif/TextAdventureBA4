using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject blackScreen;

    private static GameManager instance;

    public Player player;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }

    public GameObject terminalUI;
    public Terminal terminal;
    public TMP_InputField terminalInput;

    [HideInInspector] public bool terminalActive = false;
    [HideInInspector] public string playerName;

    public List<ItemObject> managerItems = new List<ItemObject>();

    [HideInInspector] public bool inRange = false;

    [HideInInspector] public DoorScript door;
    [HideInInspector] public bool isInRange;


    private void Awake()
    {
        //terminalUI.SetActive(false);
        //terminalInput = terminalUI.GetComponentInChildren<TMP_InputField>();
        //terminal = FindObjectOfType<Terminal>();
    }

    void Start()
    {
        StartCoroutine(OpeningScene());
    }

    public void OpenDoor()
    {
        if(door != null)
        door.OpenDoor();
    }

    void Update()
    {
        if (terminalUI.activeInHierarchy)
        {
            player.canMove = false;
        }
        else player.canMove = true;

        if (!terminalActive && !terminalUI.activeInHierarchy && Input.GetKeyDown(KeyCode.Tab))
        {
            OpenTerminal();
        }

        else if (!terminalActive && terminalUI.activeInHierarchy && Input.GetKeyDown(KeyCode.Tab))
        {
            CloseTerminal();
        }
    }

    public void OpenTerminal()
    {
        FindObjectOfType<AudioManager>().Play("TerminalOpen");

        terminalUI.SetActive(true);
        //refocus input field
        terminalInput.ActivateInputField();
        terminalInput.Select();
    }

    public void CloseTerminal()
    {
        if(terminalUI.activeInHierarchy)

        FindObjectOfType<AudioManager>().Play("TerminalClose");
        terminalUI.SetActive(false);
    }

    IEnumerator OpeningScene()
    {
        terminalActive = true;

        OpenTerminal();
        yield return new WaitForSeconds(1);
        
        terminal.AddDirectoryLine(".");
        yield return new WaitForSeconds(1);
        terminal.AddDirectoryLine("..");
        yield return new WaitForSeconds(1);
        terminal.AddDirectoryLine("...");
        yield return new WaitForSeconds(1);
        terminal.AddDirectoryLine("booting up the system...");
        yield return new WaitForSeconds(2f);
        terminal.ClearConsole();
        terminal.AddDirectoryLine("←");
        yield return new WaitForSeconds(.2f);
        terminal.ClearConsole();
        terminal.AddDirectoryLine("↖");
        yield return new WaitForSeconds(.2f);
        terminal.ClearConsole();
        terminal.AddDirectoryLine("↑");
        yield return new WaitForSeconds(.2f);
        terminal.ClearConsole();
        terminal.AddDirectoryLine("↗");
        yield return new WaitForSeconds(.2f);
        terminal.ClearConsole();
        terminal.AddDirectoryLine("→");
        yield return new WaitForSeconds(.2f);
        terminal.ClearConsole();
        terminal.AddDirectoryLine("↘");
        yield return new WaitForSeconds(.2f);
        terminal.ClearConsole();
        terminal.AddDirectoryLine("↓");
        yield return new WaitForSeconds(.2f);
        terminal.ClearConsole();
        terminal.AddDirectoryLine("↙");
        yield return new WaitForSeconds(.2f);
        terminal.ClearConsole();
        terminal.AddDirectoryLine("←");
        yield return new WaitForSeconds(.2f);
        terminal.ClearConsole();
        terminal.AddDirectoryLine("↖");
        yield return new WaitForSeconds(.2f);
        terminal.ClearConsole();
        terminal.AddDirectoryLine("↑");
        yield return new WaitForSeconds(.2f);
        terminal.ClearConsole();
        terminal.AddDirectoryLine("↗");
        yield return new WaitForSeconds(.2f);
        terminal.ClearConsole();
        terminal.AddDirectoryLine("→");
        yield return new WaitForSeconds(.2f);
        terminal.ClearConsole();
        terminal.AddDirectoryLine("↘");
        yield return new WaitForSeconds(.2f);
        terminal.ClearConsole();
        terminal.AddDirectoryLine("↓");
        yield return new WaitForSeconds(.2f);
        terminal.ClearConsole();
        terminal.AddDirectoryLine("↙");
        yield return new WaitForSeconds(.2f);
        terminal.ClearConsole();
        terminal.AddDirectoryLine("←");
        yield return new WaitForSeconds(.2f);



        yield return new WaitForSeconds(2);
        terminal.AddDirectoryLine("System rebooted.");
        yield return new WaitForSeconds(2);
        terminal.ClearConsole();
        yield return new WaitForSeconds(2);
        terminal.AddDirectoryLine("WELCOME TO THE <color=red>DUNGEON SIMULATOR 3000!</color>");
        yield return new WaitForSeconds(2);
        terminal.AddDirectoryLine("It is my honor to welcome you as our very first human test subject!!");
        yield return new WaitForSeconds(3);
        terminal.AddDirectoryLine("Isn't that just so exciting?!");
        yield return new WaitForSeconds(2);
        terminal.AddDirectoryLine("My name is Dungo. Your name is no longer of importance.");
        yield return new WaitForSeconds(3);
        terminal.AddDirectoryLine("Before we begin, we should discuss some minor details...");
        yield return new WaitForSeconds(3f);
        terminal.AddDirectoryLine("You might have some questions, but I don't want to answer them right now.");
        yield return new WaitForSeconds(3);
        terminal.AddDirectoryLine("First things first. I made this, so all credit belongs to me.");
        yield return new WaitForSeconds(3f); 
        terminal.AddDirectoryLine("And what exactly do we do here?");
        yield return new WaitForSeconds(3);
        terminal.AddDirectoryLine("This simulator was made to experiment on human test subjects.");
        yield return new WaitForSeconds(3);
        terminal.AddDirectoryLine("This would be illegal in the real world, but it's only a simulation right?");
        yield return new WaitForSeconds(3);
        terminal.AddDirectoryLine("You have absolutely nothing to worry about.");
        yield return new WaitForSeconds(3);
        terminal.AddDirectoryLine("Apart from irreversible brain damage and a tiny bit of cancer, you'll be fine.");
        yield return new WaitForSeconds(4);
        terminal.AddDirectoryLine("Alright, so.");
        yield return new WaitForSeconds(3);
        terminal.AddDirectoryLine("I will send you into the simulation and your goal is to get out.");
        yield return new WaitForSeconds(2.5f);
        terminal.AddDirectoryLine("It's really that simple.");
        yield return new WaitForSeconds(2);
        terminal.AddDirectoryLine("The way it works is just like any other video game.");
        yield return new WaitForSeconds(2);
        terminal.AddDirectoryLine("Use <color=green>WASD</color> to move");
        yield return new WaitForSeconds(3);
        terminal.AddDirectoryLine("You can open and close the terminal whenever you want by pressing <color=green>TAB</color>.");
        yield return new WaitForSeconds(3f);
        terminal.AddDirectoryLine("Use the keyworld <color=green>\"help\"</color> to get a list of commands");
        yield return new WaitForSeconds(3);
        terminal.AddDirectoryLine("There are two keys you need to find to open the doors. That's all.");
        yield return new WaitForSeconds(3f);
        terminal.AddDirectoryLine("Good luck and have fun!");
        yield return new WaitForSeconds(5);


        terminal.ClearConsole();
        terminal.AddDirectoryLine("Beginning simulation in...");
        yield return new WaitForSeconds(2);
        terminal.AddDirectoryLine("3...");
        yield return new WaitForSeconds(1);
        terminal.AddDirectoryLine("2...");
        yield return new WaitForSeconds(1);
        terminal.AddDirectoryLine("1...");
        yield return new WaitForSeconds(1);
        FindObjectOfType<AudioManager>().Play("Music");

        blackScreen.SetActive(true);
        GameManager.Instance.CloseTerminal();
        terminalActive = false;
    }
}
