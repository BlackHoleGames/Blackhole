using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveGameStatsScript : MonoBehaviour
{

    //public int MaxScore = 0;
    public bool StatusUISequence = true;
    public bool isGameOver = false;
    public bool isGoodEnding = false;
    public int playerScore = 0;
    public string[] ScoreNames;
    public string[] ScoreResults;
    public GameObject camera2Objects, cameraEndObjects, cameraStructObjects, cameraLeaderBoards;
    public GameObject InitialMusicScreen;
    public static SaveGameStatsScript GameStats;
    private String configFile;
    private String scoreFile;
    private List<GameObject> earthArray;
    public float speedToMove = 2.0f;
    public static bool SceneisChanged = true;
    private Transform earthOriginal;

    public enum GameOverType
    {
        THEEND, GAMEOVER, NOTHING
    }
    public GameOverType GOTy;
    void Awake()
    {
        Debug.Log(Application.persistentDataPath); ///AppData/LocalLow/DefaultCompany/Blackhole + ....
        configFile = Application.persistentDataPath + "/config.dat";
        scoreFile = Application.persistentDataPath + "/score.dat";
        GOTy = GameOverType.NOTHING;
        ScoreNames = new string[5];
        ScoreResults = new string[5];

        if (GameStats == null)
        {
            GameStats = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (GameStats != this)
        {
            Destroy(gameObject);
        }
        //if (InitialMusicScreen == null)
        //{
        //     == this;
        //    DontDestroyOnLoad(gameObject);
        //}else
        //{

        //}
    }


    // Use this for initialization
    void Start()
    {
        earthArray = new List<GameObject>();
        foreach (Transform child in transform)
        {
            earthArray.Add(child.gameObject);
        }
        GetStats();
        GetScore();

        earthOriginal=GameObject.FindGameObjectWithTag("Earth").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneisChanged) {
            SceneisChanged = false;
            GameObject temp = GameObject.FindGameObjectWithTag("Earth") ;
            temp.transform.position = earthOriginal.position;  //new Vector3(-195.6456f, -165.0f, 528.1803f);
            temp.transform.rotation = earthOriginal.rotation;
            //GameObject.FindGameObjectWithTag("Earth").GetComponent<Transform>().transform.eulerAngles = //new Vector3(6.583f, 289.706f, 156.25f);
        }
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1://Eartittle
                camera2Objects.SetActive(false);
                cameraLeaderBoards.SetActive(false);
                camera2Objects.SetActive(false);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().transform.position = new Vector3(-140f, -140f, 28.0f);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().fieldOfView = 60;
                InitialMusicScreen.SetActive(true);
                break;
            case 2://Tutorial
                cameraLeaderBoards.SetActive(false);
                camera2Objects.SetActive(false);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().transform.position = new Vector3(0.0f, 0.0f, -148.0f);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().fieldOfView = 60;
                InitialMusicScreen.SetActive(false);
            break;
            case 3://Menu
                cameraLeaderBoards.SetActive(false);
                camera2Objects.SetActive(false);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().transform.position = new Vector3(-140f, -140f, 28.0f);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().fieldOfView = 60;
                InitialMusicScreen.SetActive(true);
            break;
            case 4://Briefing -> Camera 3
                   //cameraLeaderBoards.SetActive(false);
                   //camera2Objects.SetActive(false);
                   //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().transform.position = new Vector3(111.9f, 14.9f, 316.0f);
                   //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().transform.eulerAngles = new Vector3(24.26f, -48.764f, 0.0f);
                   //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().fieldOfView = 32;
                   //InitialMusicScreen.SetActive(true);
                foreach (Transform earth in transform)
                {
                    earth.gameObject.SetActive(false);
                }
                InitialMusicScreen.SetActive(true);
                break;
            case 8://LeaderBoards -> Camera 2
                camera2Objects.SetActive(true);
                //GameObject.FindGameObjectWithTag("MainCamera").SetActive(false);
                //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().transform.position = new Vector3(-61.4f, -138.7f, 183.8f);
                //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().transform.eulerAngles = new Vector3(-25.085f, -29.139f, 26.167f);
                //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().fieldOfView = 60;
                break;
            case 5:
                GameObject.FindGameObjectWithTag("MainCamera").SetActive(true);
                camera2Objects.SetActive(false);
                InitialMusicScreen.SetActive(false);
                foreach (Transform earth in transform)
                {
                    earth.gameObject.SetActive(false);
                }
                break;
            case 6:
                InitialMusicScreen.SetActive(false);
                camera2Objects.SetActive(false);
                foreach (Transform earth in transform)
                {
                    earth.gameObject.SetActive(false);
                }
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().transform.position = new Vector3(-61.4f, -138.7f, 183.8f);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().transform.eulerAngles = new Vector3(-25.085f, -29.139f, 26.167f);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().fieldOfView = 60;
                break;
            default:
                foreach (Transform earth in transform)
                {
                    earth.gameObject.SetActive(true);
                }
            break;
        }
    }
    public void SetStats()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(configFile);
        DataForSave data = new DataForSave();

        //data.MaxScore = MaxScore;
        data.StatusUISequence = StatusUISequence;
        data.isGameOver = isGameOver;
        data.isGoodEnding = isGoodEnding;
        data.playerScore = playerScore;
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
            isGameOver = data.isGameOver;
            isGoodEnding = data.isGoodEnding;
            playerScore = data.playerScore;
            file.Close();
        }
        else
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
            for (int i = 0; i < 5; ++i)
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

    public void SetScore(string Player, string CurrentScore)
    {
        GetScore();
        bool found = false;
        string endingScore="";
        for (int i = 0; i < 5; i++)
        {
            if (!found)
            {
                if (int.Parse(ScoreResults[i]) <= int.Parse(CurrentScore))
                {
                    found = true;
                    endingScore = endingScore + Player + "," + CurrentScore + "\n";
                }
                else
                {
                    endingScore = endingScore + ScoreNames[i] + "," + ScoreResults[i] + "\n";
                }
            }
            else
            {
                endingScore = endingScore + ScoreNames[i - 1] + "," + ScoreResults[i - 1] + "\n";
            }
        }
        using (StreamWriter sw = new StreamWriter(scoreFile,false))// File.AppendText(scoreFile))
        {
            sw.WriteLine(endingScore);
        }
    }
    public void DefaultScore()
    {
        ScoreNames[0] = "MPM";
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
    public bool isGameOver;
    public bool isGoodEnding;
    public int playerScore;
}