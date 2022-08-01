using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class Interpreter : MonoBehaviour
{
    public InventoryObject playerInventory;
    Terminal terminal;

    public ItemObject key;

    Dictionary<string, string> colors = new Dictionary<string, string>()
    {
        {"black", "#021b21" },
        {"grey",  "#555d71" },
        {"red",   "#ff5879" },
        {"blue",  "#9ed9d8" }
    };

    List<string> response = new List<string>();

    private void Start()
    {   
        terminal = GetComponent<Terminal>();
    }

    public List<string> Interpret(string userInput)
    {
        response.Clear();

        string[] args = userInput.Split();


        if (args[0] == "help") {
            ListEntry("help", "returns a list of commands");
            ListEntry("quit", "stops the game");
            ListEntry("clear", "clears the terminal");
            ListEntry("use + item", "use item");
            ListEntry("inventory", "display current inventory");

            return response;
        }

        /*
        if (args[0] != "" && GameManager.Instance.terminalActive)
        {
            GameManager.Instance.playerName = args[0];
            response.Add("Welcome " + GameManager.Instance.playerName + "!");
            response.Add("That was the last time you will ever need your name.");
            response.Add("From now on you will be called test subject 001");
            GameManager.Instance.terminalActive = false;
            return response;
        }
        */

        if (args[0] == "inventory" || args[0] == "inv"){
            if(playerInventory.container.Count == 0){
                response.Add("Inventory is empty");
            } else{

                terminal.ClearConsole();
                response.Add(ColorString("Inventory:", "green"));
                foreach (var x in playerInventory.container)
                {
                    response.Add(x.item.name.ToString() + " [" + x.amount + "]");
                }
            }
            return response;
        }

        if (args[0] == "hello")
        {
            response.Add("Hello test subject 001!");
            response.Add("I know this might all seem a bit strange to you but just keep going");
            return response;
        }

        if (args[0] == "close")
        {
            GameManager.Instance.CloseTerminal();
        }

        if (args[0] == "quit")
        {
            Application.Quit();
        }

        if (args[0] == "ronald"){
            LoadTitle("ronald.txt", "red", 0);
            return response;
        }

        if(args.Length == 1 && args[0] == "use"){
            response.Add("wrong use syntax. use + item");
            return response;
        }

        if(args.Length == 2 && args[0] == "use"){

            if (args[1] == "key")
            {
                if (GameManager.Instance.managerItems.Contains(key)) 
                {
                    if (GameManager.Instance.inRange)
                    {
                        playerInventory.RemoveItem(key);
                        GameManager.Instance.OpenDoor();
                        response.Add("Used key");
                    }else
                    {
                        response.Add("You cannot use this here");
                    }
                   
                }
                else
                {
                    response.Add("You have no key");
                }

            }
            else
            {
                response.Add("Invalid item");
            }
            return response;
        }

        if (args.Length < 2 && args[0] == "boop"){
            response.Add("Only one boop");
            return response;
        }

        if (args.Length > 1 && args[0] == "boop" && args[1] == "boop"){
            response.Add("Two boops");
            return response;
        }

        if (args[0] == "clear"){
            terminal.ClearConsole();
            return response;
        }
        else{
            response.Add("Command not recognized. Type help for a list of commands");

            return response;
        }
    }

    public string ColorString(string s, string color)
    {
        string leftTag  = "<color=" + color + ">";
        string rightTag = "</color>";

        return leftTag + s + rightTag;
    }

    void ListEntry(string a, string b)
    {
        response.Add(ColorString(a, colors["blue"]) + ": " + ColorString(b, colors["red"]));
    }

    void LoadTitle(string path, string color, int spacing)
    {
        StreamReader file = new StreamReader(Path.Combine(Application.streamingAssetsPath, path));

        for(int i = 0; i < spacing; i++)
        {
            response.Add("");
        }

        while (!file.EndOfStream)
        {
            response.Add(ColorString(file.ReadLine(), colors[color]));
        }

        for (int i = 0; i < spacing; i++)
        {
            response.Add("");
        }

        file.Close();
    }

    public void Ronald()
    {
        LoadTitle("ronald.txt", "red", 0);
    }

}
