using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawn : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject carPrefab;
    public float minSpawnTime = 1.0f;
    public float maxSpawnTime = 5.0f;
    private float spawnTime;

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        // Wait for spawnTime seconds before spawning another car
        // spawnTime will be random
        while (true)
        {
            spawnTime = RandomGaussian(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnTime);
            Instantiate(carPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
    public static float RandomGaussian(float minValue = 0.0f, float maxValue = 1.0f)
    {
        float u, v, S;

        do
        {
            u = 2.0f * UnityEngine.Random.value - 1.0f;
            v = 2.0f * UnityEngine.Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0f);

        // Standard Normal Distribution
        float std = u * Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);

        // Normal Distribution centered between the min and max value
        // and clamped following the "three-sigma rule"
        float mean = (minValue + maxValue) / 2.0f;
        float sigma = (maxValue - mean) / 3.0f;
        return Mathf.Clamp(std * sigma + mean, minValue, maxValue);
    }
}

