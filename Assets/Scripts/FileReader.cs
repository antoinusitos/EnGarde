using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public struct DeckInfos
{
    public string deckName;
    public int deckSize;
}

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

    public Card1[] ReadFile(string deckName)
    {
        fileName = deckName + ".txt";

        Card1[] newDeck = new Card1[10];
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
                string[] action1s = infos[i].Split('=');

                int id1 = int.Parse(action1s[0]);
                int amount1 = int.Parse(action1s[1]);

                Actions action1 = null;

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

                Card1 c = new Card1();
                c.SetCard(action1);
                newDeck[index] = c;
                index++;
            }
        }

        return newDeck;
    }

    public DeckInfos[] GetDeckInfos()
    {
        fileName = "DeckAvailable.txt";

        DeckInfos[] list = null;

        StreamReader sr = new StreamReader(fullPath + "/" + fileName);
        string fileContents = sr.ReadToEnd();
        sr.Close();

        List<string> infos = new List<string>();

        string[] lines = fileContents.Split("\n"[0]);
        foreach (string line in lines)
        {
            if (line.Contains("#"))
            {
                infos.Add(line);
            }
        }

        list = new DeckInfos[infos.Count];

        for (int i = 0; i < infos.Count; i++)
        {
            string[] deckInfo = infos[i].Split('#');

            list[i].deckName = deckInfo[0];
            list[i].deckSize = int.Parse(deckInfo[1]);
        }

        return list;
    }

    public void SaveDeck(string deckName, Deck theDeck)
    {
        fileName = deckName + ".txt";

        StreamWriter sw = new StreamWriter(fullPath + "/" + fileName);

        sw.WriteLine("#Add Action with");
        sw.WriteLine("#idAction=value");
        sw.WriteLine("#idAction : (Arrow=0, Magic=1, Sword=2, Move=3, Shield=4)");
        sw.WriteLine("#value : between 1 - 5 inclusive");
        sw.WriteLine("#Action cost for deck building :");
        sw.WriteLine("#Arrow 15, Sword X * 4, Move X, Shield X, Magic X * 4");

        string line = "";

        Card1[] cards = theDeck.GetCards();

        for (int i = 0; i < cards.Length; i++)
        {
            line = "";
            Debug.Log("save:" + cards[i].GetCardType());
            line += (GetACtionID(cards[i].GetCardType()));
            line += "=";
            line += cards[i].GetCardAmount();
            sw.WriteLine(line);
        }

        sw.Flush();
        sw.Close();

        StreamReader sr = new StreamReader(fullPath + "/" + "DeckAvailable.txt");
        string fileContents = sr.ReadToEnd();
        sr.Close();

        theDeck.CheckDeckLimit();
        StreamWriter sw2 = new StreamWriter(fullPath + "/" + "DeckAvailable.txt");

        sw2.Write(fileContents);
        sw2.WriteLine(deckName + "#" + theDeck.GetLastCost());

        sw2.Flush();
        sw2.Close();
    }

    private int GetACtionID(CardType type)
    {
        switch (type)
        {
            case CardType.ARROW:
                return 0;
            case CardType.SWORD:
                return 2;
            case CardType.MOVE:
                return 3;
            case CardType.SHIELD:
                return 4;
            case CardType.MAGIC:
                return 1;
        }
        return -1;
    }

    //SINGLETON

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
