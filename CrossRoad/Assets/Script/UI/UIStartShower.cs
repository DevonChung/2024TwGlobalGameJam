using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStartShower : UIShower
{
    [SerializeField]
    EndContent Content;
    public void StartStarting()
    {
        isShowingCG = true;
        ShowPlots(Content);
    }
}