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
        fullPath = Application.dataPath + watcherFolder;
        StartReading();
    }

    public void StartReading()
    {
        ReadFile(true);
    }

    private void ReadFile(bool mainThread)
    {
        StreamReader sr = new StreamReader(fullPath + "/" + fileName);
        string fileContents = sr.ReadToEnd();
        sr.Close();

        List<string> infos = new List<string>();

        Debug.Log("Reading file " + fileName);
        string[] lines = fileContents.Split("\n"[0]);
        foreach (string line in lines)
        {
            Debug.Log(line);
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
                string[] TheInfo = infos[i].Split('=');

                switch (TheInfo[0])
                {
                    case "Exemple":
                        // DO STUFF with TheInfo[1]
                        break;
                    case "Vsync":
                        //InfoManager.GetInstance().TryReadVSyncValue(TheInfo[1], TheInfo[0]);
                        break;
                    case "ReadQuestFile":
                        //if(mainThread)
                           // QuestManager.GetInstance().SetReadquestFile(TheInfo[1], TheInfo[0]);
                        break;
                }
            }
        }
    }
}
