using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIShower : MonoBehaviour
{
    [SerializeField]
    protected Image cg;
    [SerializeField]
    protected TextMeshProUGUI text;
    protected Queue<string> plots;
    protected bool isShowingCG = false;
    protected void Update()
    {
        if (!isShowingCG) return;
        if (Input.GetMouseButtonDown(0))
        {
            ShowNextScentence();
        }
    }
    protected void ShowPlots(EndContent content)
    {
        cg.sprite = content.CG;
        plots = content.GetQueue();
        ShowNextScentence();
    }
    protected void ShowNextScentence()
    {
        if (plots.Count > 0)
        {
            string plot = plots.Dequeue();
            text.text = plot;
        }
        else
        {
            isShowingCG=false;
            EndEnding();
        }

    }
    protected virtual void EndEnding()
    {

        Debug.Log("Ending");
    }
}
