using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class FileReader : MonoBehaviour
{
    // file to read for the data
    private string fileName = "Deck1.txt";

    // folder containing the file to read
    private string watcherFolder = "/Data";

    private string fullPath = "";

    private FileSystemWatcher watcher;
    
    private void Start()
    {
    }

    public Card[] ReadFile(string deckName)
    {
        fileName = deckName + ".txt";

        Card[] newDeck = new Card[10];
        int index = 0;

        StreamReader sr = new StreamReader(fullPath + "/" + fileName);
        string fileContents = sr.ReadToEnd();
        sr.Close();

        List<string> infos = new List<string>();
        
        string[] lines = fileContents.Split("\n"[0]);
        foreach (string line in lines)
        {
            // not a comment line
            if (!line.Contains("#"))
            {
                infos.Add(line);
            }
        }

        for (int i = 0; i < infos.Count; i++)
        {
            if (!infos[i].Contains("="))
            {
                // white line
            }
            else
            {
                string[] TheInfo = infos[i].Split(new[] { "***" }, System.StringSplitOptions.None);

                string[] action1s = TheInfo[0].Split('=');
                string[] action2s = TheInfo[1].Split('=');

                int id1 = int.Parse(action1s[0]);
                int amount1 = int.Parse(action1s[1]);
                int id2 = int.Parse(action2s[0]);
                int amount2 = int.Parse(action2s[1]);

                Actions action1 = null;
                Actions action2 = null;

                switch(id1)
                {
                    case 0:
                        action1 = new Arrow();
                        break;
                    case 1:
                        action1 = new Magic();
                        break;
                    case 2:
                        action1 = new Sword();
                        break;
                    case 3:
                        action1 = new Move();
                        break;
                    case 4:
                        action1 = new Shield();
                        break;
                }
                action1.InitAction();
                action1.SetCardAmount(amount1);

                switch (id2)
                {
                    case 0:
                        action2 = new Arrow();
                        break;
                    case 1:
                        action2 = new Magic();
                        break;
                    case 2:
                        action2 = new Sword();
                        break;
                    case 3:
                        action2 = new Move();
                        break;
                    case 4:
                        action2 = new Shield();
                        break;
                }
                action2.InitAction();
                action2.SetCardAmount(amount2);

                Card c = new Card();
                c.SetCard(action1, action2);
                newDeck[index] = c;
                index++;
            }
        }

        return newDeck;
    }

    private static FileReader _instance = null;
    public static FileReader GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
        fullPath = Application.dataPath + watcherFolder;
    }
}
