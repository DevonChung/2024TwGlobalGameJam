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
        SceneTransistor transistor = FindObjectOfType<SceneTransistor>();
        transistor.OnEndTransist.AddListener(delegate { SceneManager.LoadScene("MainStage"); });
        transistor.FadeOut();
        
    }
    private void Start()
    {
        StartStarting();
    }
}