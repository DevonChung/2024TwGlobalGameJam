using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyControl : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float maxMoveSpeed = 2.0f;
    public float minMoveSpeed = -1.0f;
    public float lifetime = 60.0f; // lifetime of the GameObject
    private float time;
    private float amplitudeY = 10.0f; // Y轴移动的幅度
    private float frequencyY = 1.0f; // Y轴移动的频率
    private float x_range = 7.0f; // X轴移动的范围
    private float amplitudeX = 10f; // X轴移动的幅度
    private float[] frequenciesX; // X轴多个正弦波的频率
    private GameObject Player;
    private bool isPlaying = false;
    private GameObject audioObj;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        moveSpeed = Random.value * (maxMoveSpeed - minMoveSpeed) + minMoveSpeed;
        frequenciesX = new float[Random.Range(5, 10)];
        frequencyY = Random.value * lifetime + 1;
        for (int i = 1; i < frequenciesX.Length; i++)
        {
            frequenciesX[i] = Random.value * lifetime + 1;
        }

    }
    void Update()
    {
        if (MyGameManager.instance.currentState != MyGameManager.CrossRoadGameStatus.InGame) return;
        time += Time.deltaTime;
        if (time > lifetime)
        {
            Destroy(gameObject);
        }
        Move();
        // if monkey distance to player is less than 8.0f, play monkey sound
        if (Vector3.Distance(transform.position, Player.transform.position) < 10.0f)
        {
            isPlaying = isPlaying && audioObj != null;
            if (!isPlaying)
            {
                audioObj = AudioManager.instance.PlayMonkeyAudio();
                isPlaying = true;
            }
        }
        else
        {
            isPlaying = false;
            if (audioObj != null)
            {
                Destroy(audioObj);
            }
        }
    }

    void Move()
    {
        // 计算Y轴的速度变化
        float velocityY = -(amplitudeY * Mathf.Sin(time * frequencyY) + moveSpeed);
        velocityY = Mathf.Clamp(velocityY, -maxMoveSpeed, -minMoveSpeed);
        // 计算X轴的位置变化
        float positionX = 0f;
        foreach (var freq in frequenciesX)
        {
            positionX += Mathf.Sin(time * freq) * amplitudeX;
        }
        positionX = Mathf.Clamp(positionX, -x_range, x_range);

        // 更新物体位置
        transform.position += new Vector3(positionX, velocityY, 0) * Time.deltaTime;
    }
}
