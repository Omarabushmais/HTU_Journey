using UnityEngine;

public class soundtrigger : MonoBehaviour
{
    private AudioSource audioSource;
    private bool hasPlayed = false; // Prevents the sound from spamming

    void Start()
    {
        // Automatically fetch the AudioSource component on this GameObject
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the Player and it hasn't played yet
        if (other.CompareTag("Player") && !hasPlayed)
        {
            audioSource.Play();
            hasPlayed = true; // Mark as played so it only triggers once
        }
    }
}
