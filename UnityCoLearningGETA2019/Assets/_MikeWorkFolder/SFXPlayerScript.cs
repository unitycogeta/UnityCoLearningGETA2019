using UnityEngine;
using UnityEngine.Audio;

public class SFXPlayerScript : MonoBehaviour
{
    public AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void LoadClip(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
    }

    public void SetAudioMixer(AudioMixerGroup audioMixerGroup)
    {
        audioSource.outputAudioMixerGroup = audioMixerGroup;
    }

    
}
