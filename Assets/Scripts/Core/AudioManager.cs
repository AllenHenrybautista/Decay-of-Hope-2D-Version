using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _SFXSource;
    [SerializeField] private AudioSource _ambienceSource;
    [SerializeField] private AudioSource _combatSource;

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
    public AudioClip Hit;
    public AudioClip Slash;
    public AudioClip ZombieDeath;

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
        _SFXSource.PlayOneShot(clip);
    }

    public void PlayCombatSFX(AudioClip clip)
    {
        _combatSource.PlayOneShot(clip);
    }

    public void ToggleBGM(bool isOn)
    {
        _musicSource.mute = !isOn;
    }

    public void ToggleSFX(bool isOn)
    {
        _SFXSource.mute = !isOn;
        _combatSource.mute = !isOn;
    }

    public void SetSFXVolume(float volume)
    {
        _SFXSource.volume = volume;
    }

    public void SetCombatSFXVolume(float volume)
    {
        _combatSource.volume = volume;
    }

    public void PlaySoundEffect(string clipName)
    {
        switch (clipName)
        {
            case "shopOpen":
                PlaySFX(shopOpen);
                break;
            case "shopClose":
                PlaySFX(shopClose);
                break;
            case "select":
                PlaySFX(select);
                break;
            case "buy":
                PlaySFX(buy);
                break;
            case "sell":
                PlaySFX(sell);
                break;
            case "notEnoughMoney":
                PlaySFX(notEnoughMoney);
                break;
            case "addMoney":
                PlaySFX(addMoney);
                break;
            case "Hit":
                PlayCombatSFX(Hit);
                break;
            case "Slash":
                PlayCombatSFX(Slash);
                break;
            case "ZombieDeath":
                PlayCombatSFX(ZombieDeath);
                break;
        }
    }
}
