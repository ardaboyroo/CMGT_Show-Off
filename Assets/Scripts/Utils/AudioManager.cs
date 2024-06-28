using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static void Play2DOneShot(AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        GameObject obj = new GameObject("Sound Effect");
        AudioSource src = obj.AddComponent<AudioSource>();

        src.clip = clip;
        src.loop = false;
        src.volume = volume;
        src.pitch = pitch;
        src.Play();

        Destroy(obj, clip.length + 1f);
    }

    public static void Play3DOneShot(GameObject parent, AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        GameObject obj = new GameObject("Sound Effect");
        obj.transform.position = parent.transform.position;
        AudioSource src = obj.AddComponent<AudioSource>();

        src.clip = clip;
        src.loop = false;
        src.spatialBlend = 1f;
        src.volume = volume;
        src.pitch = pitch;
        src.Play();

        Destroy(obj, clip.length + 1f);
    }

    public static void Play2DLooping(AudioClip clip, float volume = 1f)
    {
        GameObject obj = new GameObject("Looping Sound Effect");
        AudioSource src = obj.AddComponent<AudioSource>();

        src.clip = clip;
        src.loop = true;
        src.volume = volume;
        src.Play();
    }
}
