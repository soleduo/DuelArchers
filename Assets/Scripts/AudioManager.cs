using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    public AudioSource source;
    public AudioSource bgmSource;

    public AudioClip buttonClick;
    public AudioClip chargeArrow;
    public AudioClip arrowSling;
    public AudioClip arrowHit1;
    public AudioClip arrowHit2;

    public AudioClip bgm1;
    public AudioClip bgm2;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void PlayClip(AudioClip clip)
    {
        source.PlayOneShot(clip, .8f);
    }

    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.volume = 0.1f;
        bgmSource.PlayDelayed(.8f);
    }
}
