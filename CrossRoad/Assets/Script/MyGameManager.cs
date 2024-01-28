using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyGameManager : MonoBehaviour
{
    public static MyGameManager instance;
    public enum CrossRoadGameStatus { Paused, InGame, EndGame};

    public float timeLeft = 90.0f;
    public int StartMoney = 1000;
    protected int CurrentMoney = 1000;
    public CrossRoadGameStatus currentState = CrossRoadGameStatus.Paused;
    public GameObject StartPos;
    protected GameObject Hero;
    private EndingType currentEndingType = EndingType.Normal;
    public UIManager myUIManager;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        { 
            instance = this;
        }
        updateUIInfo();
        //currentState = CrossRoadGameStatus.Paused;
        currentState = CrossRoadGameStatus.Paused;
        Hero = GameObject.FindGameObjectWithTag("Player");
        if (StartPos != null)
        { 
            Hero.transform.position = StartPos.transform.position;
        }

        //DontDestroyOnLoad(this.gameObject);
    }

    public EndingType GetEndingType()
    {
        return currentEndingType;
    }

    public void ReachEndLine()
    {
        currentState = CrossRoadGameStatus.EndGame;
        if (CurrentMoney > 0)
        {
            PlayerPrefs.SetInt("Ending", (int)EndingType.Good);
        }
        else
        {
            PlayerPrefs.SetInt("Ending", (int)EndingType.Normal);
            //currentEndingType = EndingType.Normal;
        }
        ShowResultUI();
        AudioManager.instance.Clear();
        AudioManager.instance.PlayWinAudio();
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        currentState = CrossRoadGameStatus.InGame;
        Time.timeScale = 1;
    }

    public void AddMoney(int amount)
    {
        CurrentMoney += amount;
    }

    public void LostMoney(int amount)
    {
        CurrentMoney -= amount;
    }

    protected void updateUIInfo()
    {
        if (myUIManager != null)
        {
            myUIManager.SetTime(timeLeft);
            myUIManager.SetMoney(CurrentMoney);
        }
    }

    public void ShowResultUI()
    {
        myUIManager.ShowResult(timeLeft, CurrentMoney);
    }

    protected void CheckNoTimeEvent()
    {
        if (timeLeft <= 0)
        {
            currentState = CrossRoadGameStatus.EndGame;
            myUIManager.SetText("Time is over");
            Time.timeScale = 0;
            PlayerPrefs.SetInt("Ending", (int)EndingType.Bad);
            ShowResultUI();
            AudioManager.instance.Clear();
            AudioManager.instance.PlayTimeOutAudio();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == CrossRoadGameStatus.InGame)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
            }
            else
            {
                timeLeft = 0;
            }

            updateUIInfo();
            CheckNoTimeEvent();



        }
    }
}
