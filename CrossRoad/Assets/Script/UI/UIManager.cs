using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField]
    UIPanel panel;
    [Header("Result")]
    [SerializeField]
    UIResult result;
    [Header("ReadyGo")]
    [SerializeField]
    UIReadyGo readyGo;
    [SerializeField]
    UnityEvent OnAfterReadyGo;
    [SerializeField]
    float readyTime, goTime;
    private void Awake()
    {
        readyGo.SetOnAfterReadyGo(OnAfterReadyGo);
        if (result.gameObject.active == false) return;
        result.gameObject.SetActive(false);
    }
    private void Start()
    {
        StartReadyGo();
    }
    public void SetTime(string time)
    {
        panel.SetTime(time);
    }
    public void SetTime(float time)
    {
        panel.SetTime(time);
    }
    public void SetMoney(int money)
    {
        panel.SetMoney(money);
    }
    public void SetMoney(string money)
    {
        panel.SetMoney(money);
    }
    public void SetText(string content)
    {
        panel.SetText(content);
    }
    public void ShowResult(float time, int money)
    {
        result.gameObject.SetActive(true);
        result.ShowResult(time,money);
    }
    public void StartReadyGo()
    {
        readyGo.StartReadyGo(readyTime, goTime);
    }
    public void test()
    {
        Debug.Log("start");
    }
}
