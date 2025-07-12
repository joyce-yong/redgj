using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public AudioSource audioSource;

    public void PlayClickSound()
    {
        Debug.Log("Button clicked");

        if (audioSource == null)
            Debug.LogWarning("AudioSource is NOT assigned!");
        else if (audioSource.clip == null)
            Debug.LogWarning("AudioClip is MISSING!");
        else
        {
            Debug.Log("AudioClip Name: " + audioSource.clip.name);
            Debug.Log("Playing sound...");
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
