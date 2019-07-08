using UnityEngine;

[CreateAssetMenu]
public class SongSwitchEvent : ScriptableObject
{
    [SerializeField]
    public int TrackNumber;

    private MusicPlayer musicPLayer;

    public void Raise()
    {
        musicPLayer.PlaySong(TrackNumber);
    }

    public void RegisterListener(MusicPlayer listener)
    {
        musicPLayer = listener;
    }

    public void UnRegisterListener(MusicPlayer listener)
    {
        musicPLayer = listener;
    }
}
