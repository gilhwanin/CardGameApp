using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public List<AudioClip> k = new List<AudioClip>();
    public AudioClip bgm;
    public AudioSource bgmPlayer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        bgmPlayer.clip = bgm;
        bgmPlayer.Play();
    }

    public void PlayFx(string sfxName, int a)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.clip = k[a];
        audiosource.Play();

        Destroy(go, audiosource.clip.length);
    }
}