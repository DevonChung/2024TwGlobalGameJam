using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStartShower : UIShower
{
    [SerializeField]
    EndContent Content;
    public void StartStarting()
    {
        isShowingCG = true;
        ShowPlots(Content);
    }
    protected override void EndEnding()
    {
        SceneManager.LoadScene("MainStage");
    }
    private void Start()
    {
        StartStarting();
    }
}