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
    public GameObject cameraUI;
    public static SaveGameStatsScript GameStats;
    private String configFile;
    private String scoreFile;
    private List<GameObject> earthArray;
    public float speedToMove = 2.0f;
    private float Scene3_incrementPositionX = 1.0f;
    private float Scene3_incrementAngleY = 0.25f;
    private float Scene3_zPosition = 147.0f;
    private float Scene3_maxPositionX = 225f;
    private float Scene3_maxFieldofView = 32f;

    private bool Scene4_isupdated = false;
    private float Scene4_incrementPositionX = 1.0f;
    private float Scene4_incrementAngleY = 0.25f;
    private float Scene4_zPosition = 147.0f;
    private float Scene4_maxPositionX = 563f;
    private float Scene4_maxFieldofView = 32f;

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
    }

    // Update is called once per frame
    void Update()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 3:
                foreach (Transform earth in transform)
                {
                    earth.gameObject.SetActive(true);
                }
                if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().transform.position.x < Scene3_maxPositionX)
                {
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().transform.position = new Vector3(
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().transform.position.x + Scene3_incrementPositionX, 0.0f, -Scene3_zPosition);

                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().transform.eulerAngles = new Vector3(
                    0.0f, GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().transform.eulerAngles.y - Scene3_incrementAngleY, 0.0f);
                }
                if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().fieldOfView > Scene3_maxFieldofView)
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().fieldOfView = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().fieldOfView - 0.5f;
                //Positions to be Done.
                //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().transform.position = new Vector3(225f, 0.0f, -147.0f);
                //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().transform.eulerAngles = new Vector3(0.0f, -58.0f, 0.0f);
                //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().fieldOfView = 32;
                break;
            case 4:
                if (!Scene4_isupdated)
                {
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().transform.position = new Vector3(Scene3_maxPositionX, 0.0f, -Scene3_zPosition);
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().transform.eulerAngles = new Vector3(0.0f, -58.0f, 0.0f);
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().fieldOfView = Scene3_maxFieldofView;
                }
                if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().transform.position.x < Scene4_maxPositionX)
                {
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().transform.position = new Vector3(
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().transform.position.x + Scene3_incrementPositionX, 0.0f, -Scene3_zPosition);
                }
                if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().fieldOfView > Scene4_maxFieldofView)
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().fieldOfView = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().fieldOfView - 0.5f;
            //Positions to be Done.
            //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().transform.position = new Vector3(563f, -7f, -374.5f);
            //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().transform.eulerAngles = new Vector3(0.0f, -56.0f, 0.0f);
            //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().fieldOfView = 3;
            break;
            case 5:
                foreach (Transform earth in transform)
                {
                    earth.gameObject.SetActive(false);
                }
                break;
            default:
                foreach (Transform earth in transform)
                {
                    earth.gameObject.SetActive(true);
                }
            break;
        }
        if(SceneManager.GetActiveScene().buildIndex == 5)
        {

        }else
        {

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
    public bool isGameOver;
    public bool isGoodEnding;
    public int playerScore;
}