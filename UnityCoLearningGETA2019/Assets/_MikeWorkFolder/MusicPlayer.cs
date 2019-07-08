using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class MusicPlayer : ScriptableObject
{
    [SerializeField]
    private List<SongSwitchEvent> songSwitchEvents;

    [SerializeField]
    public SongList songlist;

    [SerializeField]
    public AudioSource music;

    private void OnDisable()
    {
        UnregisterSongSwitchEvents();
    }

    private void Awake()
    {
        //music = new AudioSource();
        GetSongSwitchEvents();
    }

    public void PlaySong(int trackNumber)
    {
        music.Stop();
        music.clip = songlist.Collection[trackNumber].audioCLip;
        music.Play();
    }

    private void GetSongSwitchEvents()
    {
        SongSwitchEvent[] instances = Resources.FindObjectsOfTypeAll<SongSwitchEvent>();
        for(int i = 0; i < instances.Length; i++)
        {
            songSwitchEvents.Add(instances[i]);
            instances[i].RegisterListener(this);
        }
    }

    private void UnregisterSongSwitchEvents()
    {
        SongSwitchEvent[] instances = Resources.FindObjectsOfTypeAll<SongSwitchEvent>();
        for (int i = 0; i < instances.Length; i++)
        {
            instances[i].UnRegisterListener(this);
        }
    }
}
