using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
