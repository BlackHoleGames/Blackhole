﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveGameStatsScript : MonoBehaviour {

    //public int MaxScore = 0;
    public bool StatusUISequence = true;
    public string[] ScoreNames;
    public string[] ScoreResults;
    public static SaveGameStatsScript GameStats;
    private String configFile;
    private String scoreFile;
    void Awake()
    {
        Debug.Log(Application.persistentDataPath); ///AppData/LocalLow/DefaultCompany/Blackhole + ....
        configFile = Application.persistentDataPath + "/config.dat";
        scoreFile = Application.persistentDataPath + "/score.dat";
        ScoreNames = new string[5];
        ScoreResults = new string[5];
        if (GameStats == null)
        {
            GameStats = this;
            DontDestroyOnLoad(gameObject);
        }else if (GameStats!= this)
        {
            Destroy(gameObject);
        }
    }


	// Use this for initialization
	void Start () {
        GetStats();
        GetScore();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetStats()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(configFile);
        DataForSave data = new DataForSave();

        //data.MaxScore = MaxScore;
        data.StatusUISequence = StatusUISequence;
        
        bf.Serialize(file, data);

        file.Close();
    }
    public void GetStats()
    {
        if (File.Exists(configFile))
        {
            
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(configFile, FileMode.Open);

            DataForSave data = (DataForSave)bf.Deserialize(file);

            //MaxScore = data.MaxScore;
            StatusUISequence = data.StatusUISequence;

            file.Close();
        }else
        {
            StatusUISequence = true;
        }
    }

    public void GetScore()
    {
        if (File.Exists(scoreFile))
        {
            StreamReader reader;
            FileInfo file;
            file = new FileInfo(scoreFile);
            reader = file.OpenText();
            for (int i = 0; i< 5; ++i)
            {
                string text = reader.ReadLine();
                string[] processedText = text.Split(',');
                ScoreNames[i] = processedText[0];
                ScoreResults[i] = processedText[1];
            }
            reader.Close();
        }
        else
        {
            DefaultScore();
        }
    }

    public void SetScore(string Player,string CurrentScore)
    {
        GetScore();
        File.Delete(scoreFile);
        File.Create(scoreFile);
        bool found = false;
        for(int i = 0; i < 5; i++)
        {
            if (!found) { 
                if (int.Parse(ScoreResults[i]) <= int.Parse(CurrentScore))
                {
                    found = true;
                    using (StreamWriter sw = File.AppendText(scoreFile))
                        sw.WriteLine(Player + "," + CurrentScore);
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(scoreFile))
                        sw.WriteLine(ScoreNames[i]+","+ScoreResults[i]);
                    
                }
            }else
            {
                using (StreamWriter sw = File.AppendText(scoreFile))
                    sw.WriteLine(ScoreNames[i-1] + "," + ScoreResults[i-1]);
            }
        }
    }
    public void DefaultScore()
    {
        ScoreNames[0] = "MPP";
        ScoreNames[1] = "EMI";
        ScoreNames[2] = "FPA";
        ScoreNames[3] = "AM3";
        ScoreNames[4] = "CSP";

        ScoreResults[0] = "2000000";
        ScoreResults[1] = "1000000";
        ScoreResults[2] = "500000";
        ScoreResults[3] = "250000";
        ScoreResults[4] = "10000";

        using (StreamWriter sw = File.CreateText(scoreFile))
        {
            sw.WriteLine(ScoreNames[0] + "," + ScoreResults[0]);
            sw.WriteLine(ScoreNames[1] + "," + ScoreResults[1]);
            sw.WriteLine(ScoreNames[2] + "," + ScoreResults[2]);
            sw.WriteLine(ScoreNames[3] + "," + ScoreResults[3]);
            sw.WriteLine(ScoreNames[4] + "," + ScoreResults[4]);
        }
    }
}
[Serializable]
class DataForSave
{
    public bool StatusUISequence;
}
