using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _SFXSource;
    [SerializeField] private AudioSource _ambienceSource;

    [Header("Audio Clips")]
    public AudioClip bgm;
    public AudioClip ambience;
    public AudioClip shopOpen;
    public AudioClip shopClose;
    public AudioClip select;
    public AudioClip buy;
    public AudioClip sell;
    public AudioClip notEnoughMoney;
    public AudioClip addMoney;

    private void Start()
    {
        PlayMusic(bgm);
        PlayAmbience(ambience);
    }

    public void PlayMusic(AudioClip clip)
    {
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void PlayAmbience(AudioClip clip)
    {
        _ambienceSource.clip = clip;
        _ambienceSource.Play();
    }


    public void PlaySFX(AudioClip clip)
    {
        _SFXSource.clip = clip;
        _SFXSource.Play();
    }

    public void ToggleBGM(bool isOn)
    {
        _musicSource.mute = !isOn;
    }

    public void ToggleSFX(bool isOn)
    {
        _SFXSource.mute = !isOn;
    }
}
