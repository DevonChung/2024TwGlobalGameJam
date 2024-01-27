using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    UIPanel panel;
    [SerializeField]
    UIResult result;

    private void Awake()
    {
        result.gameObject.SetActive(false);
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
}
