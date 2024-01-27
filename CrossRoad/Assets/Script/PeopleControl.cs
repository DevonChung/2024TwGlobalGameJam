using UnityEngine;

public class PeopleControl : MonoBehaviour
{
    public Vector3 targetScale;
    public Vector3 targetPosition;
    public float duration;
    private float dx;
    private float ds;
    public float lastTime;

    // Start is called before the first frame update
    void Start()
    {
        SetRandomTarget();
    }

    // Update is called once per frame
    void Update()
    {
        // Scale the transform
        transform.localScale += new Vector3(ds * Time.deltaTime, 0, 0);
        transform.localPosition += new Vector3(dx * Time.deltaTime, 0, 0);
        lastTime -= Time.deltaTime;
        
        if (lastTime <= 0)
        {
            SetRandomTarget();
        }
    }

    void SetRandomTarget()
    {
        // Set a random duration between 1.0f and 3.0f
        duration = Random.Range(1.0f, 3.0f);

        // Set a random target scale between 0.5f and 5.0f
        float scale = Random.Range(0.5f, 5.0f);
        targetScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);

        // Set the target position such that x is 1 + scale / 2
        targetPosition = new Vector3(1 + scale / 2, transform.localPosition.y, transform.localPosition.z);
        ds = (scale - transform.localScale.x) / duration;
        dx = (targetPosition.x - transform.localPosition.x) / duration;
        lastTime = duration;
    }
}