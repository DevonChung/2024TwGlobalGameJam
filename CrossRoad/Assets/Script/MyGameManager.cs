using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyGameManager : MonoBehaviour
{
    public enum CrossRoadGameStatus { Paused, InGame, EndGame};

    public float timeLeft = 90.0f;
    public int StartMoney = 1000;
    protected int CurrentMony = 1000;
    protected CrossRoadGameStatus currentState = CrossRoadGameStatus.Paused;
    public GameObject StartPos;
    protected GameObject Hero;

    public UIManager myUIManager;

    // Start is called before the first frame update
    void Start()
    {
        //currentState = CrossRoadGameStatus.Paused;
        currentState = CrossRoadGameStatus.InGame;
        Hero = GameObject.FindGameObjectWithTag("Player");
        if (StartPos != null)
        { 
            Hero.transform.position = StartPos.transform.position;
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
            if (myUIManager != null)
            {
                myUIManager.SetTime(timeLeft);
            }

            if (timeLeft <= 0 )
            {
                currentState = CrossRoadGameStatus.EndGame;
                myUIManager.SetText("Time is over");
            }

        }
    }
}
