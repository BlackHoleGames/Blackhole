using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveGameStatsScript : MonoBehaviour {

    //public int MaxScore = 0;
    public bool StatusUISequence = true;

    public static SaveGameStatsScript GameStats;
    private String configFile;
    private String scoreFile;
    void Awake()
    {
        Debug.Log(Application.persistentDataPath); ///AppData/LocalLow/DefaultCompany/Blackhole + ....
        configFile = Application.persistentDataPath + "/config.dat";
        scoreFile = Application.persistentDataPath + "/score.dat";

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
}
[Serializable]
class DataForSave
{
    //public int MaxScore;
    public bool StatusUISequence;
    //public DataForSave(int MaxScore)
    //{
    //    this.MaxScore = MaxScore;
    //}
}
