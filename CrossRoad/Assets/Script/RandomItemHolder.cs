using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemHolder : MonoBehaviour
{
    public List<GameObject> ItemPool;

    // Start is called before the first frame update
    void Start()
    {
        int iPoolSize = ItemPool.Count;
    
        if (iPoolSize > 0)
        {
            int idx = Random.Range(0, iPoolSize);
            
            GameObject myObj = Instantiate(ItemPool[idx], this.transform.position, this.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
