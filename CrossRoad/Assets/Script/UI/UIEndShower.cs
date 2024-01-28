using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIEndShower : UIShower
{
    [SerializeField]
    EndContent GoodContent, NormalContent, BadContent;

    private void Start()
    {
        int iEndType = PlayerPrefs.GetInt("Ending");
        StartEnding((EndingType)iEndType);
    }

    public void StartEnding(EndingType type)
    {
        isShowingCG = true;
        switch (type)
        {
            case EndingType.Good:
                ShowPlots(GoodContent);
                break;
            case EndingType.Normal:
                ShowPlots(NormalContent);
                break;
            case EndingType.Bad:
                ShowPlots(BadContent);
                break;
        }
    }
    protected override void EndEnding()
    {
        SceneManager.LoadScene("Title");
    }
}
public enum EndingType
{
    Good,
    Normal,
    Bad
}