using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class SFXPlayer : ScriptableObject
{
    [SerializeField]
    public AudioSource sound;

    [SerializeField]
    private SongList sfx;

    public void LoadClip(AudioClip audioClip)
    {
        sound.clip = audioClip;
        Play();
    }

    public void Play()
    {
        sound.Play();
    }

    /*
    public void SetAudioMixer(AudioMixerGroup audioMixerGroup)
    {
        audioSource.outputAudioMixerGroup = audioMixerGroup;
    }
    */
}
