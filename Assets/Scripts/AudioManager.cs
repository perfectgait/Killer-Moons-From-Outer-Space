using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    [SerializeField] Sound[] soundEffects;
    [SerializeField] Sound[] musicTracks;

    private void Awake()
    {
        if (instance != null)
        {
            if (instance == this)
            {
                Destroy(this);
            }
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        InitializeSounds("SFX", soundEffects);
        InitializeSounds("Music", musicTracks);
    }

    private void InitializeSounds(string prefix, Sound[] sounds)
    {
        foreach (Sound sound in sounds)
        {
            GameObject _go = new GameObject(prefix + " - " + sound.GetName());
            sound.SetSource(_go.AddComponent<AudioSource>());
            _go.transform.parent = transform;
        }
    }

    public void PlaySoundEffect(string name)
    {
        PlaySound(name, soundEffects);
    }

    public void PlayMusic(string name)
    {
        StopCurrentlyPlayingMusic();
        PlaySound(name, musicTracks);
    }

    private void PlaySound(string name, Sound[] soundArray)
    {
        foreach (Sound sound in soundArray)
        {
            if (sound.GetName() == name)
            {
                sound.Play();
                return;
            }
        }
    }

    private void StopCurrentlyPlayingMusic()
    {
        foreach (Sound track in musicTracks)
        {
            if (track.IsPlaying())
            {
                track.Stop();
            }
        }
    }
}

[System.Serializable]
public class Sound
{
    [SerializeField] string name;
    [SerializeField] AudioClip audioClip;
    [Range(0f, 1f)]
    [SerializeField] float volume = 1f;
    [Range(-0.5f, 1.4f)]
    [SerializeField] float pitch = 1f;
    [SerializeField] bool loop = false;

    private AudioSource audioSource;

    public string GetName()
    {
        return name;
    }

    public void SetSource(AudioSource _audioSource)
    {
        audioSource = _audioSource;
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.loop = loop;
    }

    public void Play()
    {
        audioSource.Play();
    }
    public void Stop()
    {
        audioSource.Stop();
    }

    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }

}
