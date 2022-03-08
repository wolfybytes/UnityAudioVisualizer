using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AutoPlaylistOrganizer : MonoBehaviour
{
    public static AutoPlaylistOrganizer instance;

    private AudioSource source;

    public AudioClip[] tracks;
    public int currentTrackIndex = 0;

    public delegate void OnUpdateIndex(int index);
    public OnUpdateIndex onUpdateAudioTrack;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);


        tracks = new AudioClip[1];
        tracks[0] = Resources.Load<AudioClip>("Hi I'm Case & Rover Red - The Green Blues - 04 We'd Joke");
        source = GetComponent<AudioSource>();
        
        UpdateAudioTrack(0);
    }

    public void Rewind()
    {
        int nextIndex = (currentTrackIndex - 1 == 0) ? tracks.Length - 1 : currentTrackIndex - 1;
        UpdateAudioTrack(nextIndex);
    }

    public void PlayPause()
    {
        if (source.isPlaying) {
            source.Pause();
        } else {
            source.Play();
        }
    }

    public void Skip()
    {
        int nextIndex = (currentTrackIndex + 1) % tracks.Length;
        UpdateAudioTrack(nextIndex);
    }

    public void UpdateAudioTrack(int index)
    {
        currentTrackIndex = index;
        source.Play();
        onUpdateAudioTrack?.Invoke(index);
    }

    public AudioClip GetCurrentAudioTrack()
    {
        return tracks[currentTrackIndex];
    }
}
