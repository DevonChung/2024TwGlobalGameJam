using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public GameObject CracrashAudio;
    public GameObject Bgm;
    public GameObject PoliceStationAudio;
    public GameObject UfoAudio;
    public GameObject AcAudio;
    public GameObject DrugAudio;
    public GameObject Monkey1Audio;
    public GameObject Monkey2Audio;
    public GameObject Monkey3Audio;

    void Awake()
    {
        instance = this;
    }

    IEnumerator StopAudio(float duration, GameObject audioObject)
    {
        yield return new WaitForSeconds(duration);
        Destroy(audioObject);
    }

    public GameObject PlayCrashAudio()
    {
        GameObject audioObj = Instantiate(CracrashAudio, transform);
        audioObj.AddComponent<AutoDestroyAudio>();
        return audioObj;
    }

    public void PlayBgm()
    {
        Instantiate(Bgm, transform);
    }

    public GameObject PlayPoliceStationAudio()
    {
        GameObject audioObject = Instantiate(PoliceStationAudio, transform);
        audioObj.AddComponent<AutoDestroyAudio>();
        return audioObj;
    }
    public GameObject PlayUfoAudio()
    {
        GameObject audioObject = Instantiate(UfoAudio, transform);
        audioObject.AddComponent<AutoDestroyAudio>();
        return audioObj;
    }
    public GameObject PlayAcAudio()
    {
        GameObject audioObject = Instantiate(AcAudio, transform);
        audioObject.AddComponent<AutoDestroyAudio>();
        return audioObj;
    }
    public void PlayDrugAudio()
    {
        GameObject audioObject = Instantiate(DrugAudio, transform);
        StartCoroutine(StopAudio(5.0f,audioObject));
    }
    public GameObject PlayMonkeyAudio()
    {
        int randomIndex = Random.Range(0, 3);
        switch (randomIndex)
        {
            case 0:
                GameObject audioObject = Instantiate(Monkey1Audio, transform);
                audioObject.AddComponent<AutoDestroyAudio>();
                break;
            case 1:
                GameObject audioObject = Instantiate(Monkey2Audio, transform);
                audioObject.AddComponent<AutoDestroyAudio>();
                break;
            case 2:
                GameObject audioObject = Instantiate(Monkey3Audio, transform);
                audioObject.AddComponent<AutoDestroyAudio>();
                break;
            default:
                GameObject audioObject = Instantiate(Monkey1Audio, transform);
                audioObject.AddComponent<AutoDestroyAudio>();
                break;
        }
        return audioObj;
    }
}
