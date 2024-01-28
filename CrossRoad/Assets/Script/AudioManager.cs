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
    public GameObject MoneyAudio;

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
    public void Clear() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }
    public void PlayBgm()
    {
        Instantiate(Bgm, transform);
    }

    public GameObject PlayPoliceStationAudio()
    {
        GameObject audioObj = Instantiate(PoliceStationAudio, transform);
        audioObj.AddComponent<AutoDestroyAudio>();
        return audioObj;
    }
    public GameObject PlayUfoAudio()
    {
        GameObject audioObj = Instantiate(UfoAudio, transform);
        audioObj.AddComponent<AutoDestroyAudio>();
        return audioObj;
    }
    public GameObject PlayAcAudio()
    {
        GameObject audioObj = Instantiate(AcAudio, transform);
        audioObj.AddComponent<AutoDestroyAudio>();
        return audioObj;
    }
    public void PlayDrugAudio()
    {
        GameObject audioObj = Instantiate(DrugAudio, transform);
        StartCoroutine(StopAudio(5.0f, audioObj));
    }
    public GameObject PlayMonkeyAudio()
    {
        int randomIndex = Random.Range(0, 3);
        GameObject audioObj;
        switch (randomIndex)
        {
            case 0:
                audioObj = Instantiate(Monkey1Audio, transform);
                audioObj.AddComponent<AutoDestroyAudio>();
                break;
            case 1:
                audioObj = Instantiate(Monkey2Audio, transform);
                audioObj.AddComponent<AutoDestroyAudio>();
                break;
            case 2:
                audioObj = Instantiate(Monkey3Audio, transform);
                audioObj.AddComponent<AutoDestroyAudio>();
                break;
            default:
                audioObj = Instantiate(Monkey1Audio, transform);
                audioObj.AddComponent<AutoDestroyAudio>();
                break;
        }
        return audioObj;
    }
    public GameObject PlayMoneyAudio()
    {
        GameObject audioObj = Instantiate(MoneyAudio, transform);
        audioObj.AddComponent<AutoDestroyAudio>();
        return audioObj;
    }
}
