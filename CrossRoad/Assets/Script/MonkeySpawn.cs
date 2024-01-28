using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeySpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] spawnPoints;
    public GameObject monkeyPrefab;
    public float spawnTime = 10.0f;
    private float time = 0.0f;
    void Start()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
        randomSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > spawnTime)
        {
            randomSpawn();
            time = 0.0f;
            if (spawnTime <= 5.0f){
                randomSpawn();
            } 
            else
                spawnTime -= 1.0f;
        }
    }
    void randomSpawn()
    {
        int index = Random.Range(0, spawnPoints.Length);
        GameObject monkey = Instantiate(monkeyPrefab, transform);
        // monkey.GetComponent<MonkeyControl>().Start();
    }
}
