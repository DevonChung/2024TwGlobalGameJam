using UnityEngine;

public class AutoDestroyAudio : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component missing from this GameObject");
            Destroy(gameObject);
        }
        else if (!audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
