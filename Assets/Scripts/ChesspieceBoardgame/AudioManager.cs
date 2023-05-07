
using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public Sound[] musicSounds;
    public Sound[] sfxSounds;

    AudioSource musicSource;
    AudioSource sfxSource;

    private void Awake()
    {
        musicSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        sfxSource = GameObject.Find("SFXSource").GetComponent<AudioSource>();
    }


    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        musicSource.clip = s.clip;
        musicSource.volume = s.volumn;
        musicSource.pitch = s.pitch;
        musicSource.loop = s.loop;
        musicSource.Play();
    }


    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        sfxSource.clip = s.clip;
        sfxSource.volume = s.volumn;
        sfxSource.pitch = s.pitch;
        sfxSource.loop = s.loop;
        sfxSource.Play();
    }
}
