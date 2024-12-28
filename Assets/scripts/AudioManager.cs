using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void SetVolume(string name, float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = volume;
    }

    public void FadeOut(string name)
    {
        StartCoroutine(_FadeOut(name));
    }

    public void FadeIn(string name)
    {
        StartCoroutine(_FadeIn(name));
    }

    IEnumerator _FadeOut(string name)
    {
        float timeToFade = .5f;

        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = Mathf.Lerp(s.source.volume, 0f, timeToFade);
        yield return new WaitForSeconds(timeToFade);
    }

    IEnumerator _FadeIn(string name)
    {
        float timeToFade = .5f;

        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = Mathf.Lerp(0f, .03f, timeToFade);
        yield return new WaitForSeconds(timeToFade);
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
}
