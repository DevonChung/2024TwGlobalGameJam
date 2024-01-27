using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyGameManager : MonoBehaviour
{
    public enum CrossRoadGameStatus { Paused, InGame};

    public float timeLeft = 90;
    public int StartMoney = 1000;
    protected int CurrentMony = 1000;
    protected CrossRoadGameStatus currentState = CrossRoadGameStatus.Paused;
    public GameObject StartPos;
    protected GameObject Hero;

    // Start is called before the first frame update
    void Start()
    {
        currentState = CrossRoadGameStatus.Paused;
        Hero = GameObject.FindGameObjectWithTag("Player");
        if (StartPos != null)
        { 
            Hero.transform.position = StartPos.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
