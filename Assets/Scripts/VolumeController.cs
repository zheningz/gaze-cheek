using UnityEngine;
using Gazecheek;
using Gazecheek.Scripts;

[RequireComponent(typeof(AudioSource))]
public class VolumeController : MonoBehaviour
{
    private EyeInteractable eyeInteractable;
    private AudioSource audioSource;

    void Start()
    {
        eyeInteractable = GetComponent<EyeInteractable>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }

    void Update()
    {
        audioSource.volume = eyeInteractable.Value;
    }
}
