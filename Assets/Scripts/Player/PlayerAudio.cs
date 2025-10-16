using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudio : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip collisionClip;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayCollisionSound()
    {
        if (collisionClip != null)
            audioSource.PlayOneShot(collisionClip);
        else
            EditorApplication.Beep();
    }

    public void PlayCustomSound(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }
}
