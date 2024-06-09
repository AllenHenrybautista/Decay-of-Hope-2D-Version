using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SettingsManager : MonoBehaviour
{
    AudioManager audioManager;

    public GameObject SettingsUI;
    public GameObject BGMToggle;
    public GameObject SFXToggle;
    public GameObject Resume;
    public GameObject Exit;
    public bool SettingsActive;
    public AudioSource audioSource;


    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        SettingsUI.SetActive(false);
    }

    private void Update()
    {
        OpenSettings();
    }

    public void OpenSettings()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            if (!SettingsActive)
            {
                Pause();
                SettingsActive = true;
            }
            else
            {
                ResumeClicked();
                SettingsActive = false;
            }
        }
    }

    public void Pause()
    {
        audioManager.PlaySFX(audioManager.select);
        SettingsUI.SetActive(true);
        SettingsActive = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }

    public void BGMToggleClicked()
    {
        bool isBGMOn = BGMToggle.GetComponent<Toggle>().isOn;
        audioManager.PlaySFX(audioManager.select);
        audioManager.ToggleBGM(isBGMOn);
    }

    public void SFXToggleClicked()
    {
        bool isSFXOn = SFXToggle.GetComponent<Toggle>().isOn;
        audioManager.PlaySFX(audioManager.select);
        audioManager.ToggleSFX(isSFXOn);
    }

    public void ResumeClicked()
    {
        SettingsUI.SetActive(false);
        SettingsActive = false;
        audioManager.PlaySFX(audioManager.select);
        Time.timeScale = 1;
    }

    public void ExitClicked()
    {
        //UnityEditor.EditorApplication.isPlaying = false; - for Editor Testing
        Application.Quit();
    }
}
