using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEndShower : MonoBehaviour
{
    [SerializeField]
    EndContent GoodContent, NormalContent, BadContent;
    [SerializeField]
    Image cg;
    [SerializeField]
    TextMeshProUGUI text;
    Queue<string> plots;
    bool isShowingCG=false;
    public void StartEnding(EndingType type)
    {
        isShowingCG = true;
        switch (type)
        {
            case EndingType.Good:
                ShowEnding(GoodContent);
                break;
            case EndingType.Normal:
                ShowEnding(NormalContent);
                break;
            case EndingType.Bad:
                ShowEnding(BadContent);
                break;
        }
    }
    private void Update()
    {
        if (!isShowingCG) return;
        if (Input.GetMouseButtonDown(0))
        {
            ShowNextScentence();
        }
    }
    private void ShowEnding(EndContent content)
    {
        cg.sprite = content.CG;
        plots = content.GetQueue();
        ShowNextScentence();
    }
    private void ShowNextScentence()
    {
        if(plots.Count > 0)
        {
            string plot = plots.Dequeue();
            text.text = plot;
        }
        else
        {
            EndEnding();
        }
        
    }
    private void EndEnding()
    {
        Debug.Log("Ending");
    }
    private void Awake()
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