using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputCtrlUI : MonoBehaviour
{
    public int scene = 0;
    public AudioClip soundPlay;
    private IEnumerator PlayFile;
    public GameObject UIMenu, UIPressStart, EventSystemStart, EventSystemMenu;
    private AudioSource source1 { get { return GetComponent<AudioSource>(); } }
    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        source1.clip = soundPlay;
        source1.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("enter") ||
            Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Start"))
        {
            PlayFile = playFileStart();
            StartCoroutine(PlayFile);
        }
    }
    IEnumerator playFileStart()
    {
        source1.PlayOneShot(soundPlay);
        yield return new WaitForSeconds(0.5f);
        switch (scene)
        {
            case 1:
                if (SaveGameStatsScript.GameStats.StatusUISequence) SaveGameStatsScript.GameStats.StatusUISequence = false;
                else SaveGameStatsScript.GameStats.StatusUISequence = true;
                SaveGameStatsScript.GameStats.isGameOver = false;
                SaveGameStatsScript.GameStats.playerScore = 0;
                SaveGameStatsScript.GameStats.SetStats();
                SaveGameStatsScript.GameStats.GetStats();
                UIPressStart.SetActive(false);
                EventSystemStart.SetActive(false);
                
                UIMenu.SetActive(true);
                EventSystemMenu.SetActive(true);
                scene = 0;
                break;
            case 2:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                scene = 0;
                break;
            case 3: 
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                scene = 0;
                break;
            case 4: //Briefing
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                scene = 0;
                break;
            case 5: //End
                SceneManager.LoadScene(6);
                scene = 0;
            break;
            case 6: //LeaderBoard

                if (!SaveGameStatsScript.GameStats.isGameOver) SceneManager.LoadScene(2);
                scene = 0;
            break;
            case 7: //GameOver
                SceneManager.LoadScene(6);
                scene = 0;
            break;
            case 9: //History
                //if (SaveGameStatsScript.GameStats.StatusUISequence) SaveGameStatsScript.GameStats.StatusUISequence = false;
                //else SaveGameStatsScript.GameStats.StatusUISequence = true;
                SceneManager.LoadScene(2);
                scene = 0;
                break;
            case 10: //EndGame
                SceneManager.LoadScene(8);
                scene = 0;
            break;
            default:
                scene = 0;
                break;
        }
    }
}
