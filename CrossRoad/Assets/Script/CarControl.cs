using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{

    public float moveSpeed = 3.0f;
    public float liveTime = 10.0f;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        liveTime -= Time.deltaTime;
        if (liveTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
