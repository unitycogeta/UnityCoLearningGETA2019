using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SongList : ScriptableObject
{
    [SerializeField]
    public List<Song> Collection;

    /*
    [SerializeField]
    public List<AudioClip> Collection;
    public List<float> Volume;
    */
}

[System.Serializable]
public class Song
{
    [SerializeField]
    public AudioClip audioCLip;

    [SerializeField]
    public float volume;
}
  