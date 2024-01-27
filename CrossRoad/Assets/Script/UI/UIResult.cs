using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class UIResult : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI TimeText, MoneyText;
    public void ShowResult(float time, int money)
    {
        SetTime(time);
        SetMoney(money);
    }
    public void SetTime(string time)
    {
        TimeText.text = time;
    }
    public void SetTime(float time)
    {
        int minute = ((int)time % 60);
        int second = ((int)time / 60);
        string min = minute.ToString(), sec = second.ToString();
        if (second < 10) sec = "0" + sec;
        if (minute < 10) min = "0" + min;
        TimeText.text = $"{min}:{sec}";
    }
    public void SetMoney(int money)
    {
        MoneyText.text = money.ToString();
    }
    public void SetMoney(string money)
    {
        MoneyText.text = money;
    }
    public void Next()
    {
        //SceneManager.LoadScene();
    }
}
