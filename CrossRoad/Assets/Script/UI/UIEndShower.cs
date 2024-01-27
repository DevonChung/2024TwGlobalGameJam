using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEndShower : UIShower
{
    [SerializeField]
    EndContent GoodContent, NormalContent, BadContent;

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
        Debug.Log("Ending");
    }
    private void Start()
    {
        StartEnding(EndingType.Good);
    }
}
public enum EndingType
{
    Good,
    Normal,
    Bad
}