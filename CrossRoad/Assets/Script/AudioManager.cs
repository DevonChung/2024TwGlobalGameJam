using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public GameObject CracrashAudio;
    void Awake()
    {
        instance = this;
    }
    
    public void PlayCrashAudio()
    {
        Instantiate(CracrashAudio, transform);
    }
}
