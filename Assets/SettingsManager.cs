using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingsManager : MonoBehaviour
{

    public AudioMixer Master;

    public bool Fullscreen = true;
    public int ResolutionIndex = 0;

    public Slider MasterSldier;
    public Slider MusicSlider;
    public Slider SFXSlider;
    public Slider AmbientSlider;
    public Slider UISlider;
    public Slider VoiceSlider;

    public Toggle FullscreenToggle;
    public TMP_Dropdown ResolutionDropdown;

    // Start is called before the first frame update
    public void InstantiateAllSettings()
    {
        MasterSldier.value = PlayerPrefs.GetFloat("MasterVolume", 0.7f);
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.7f);
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.7f);
        AmbientSlider.value = PlayerPrefs.GetFloat("AmbientVolume", 0.7f);
        UISlider.value = PlayerPrefs.GetFloat("UIVolume", 0.7f);
        VoiceSlider.value = PlayerPrefs.GetFloat("VoiceVolume", 0.7f);
        Fullscreen = Convert.ToBoolean(PlayerPrefs.GetInt("Fullscreen", 1));
        FullscreenToggle.isOn = Fullscreen;
        ResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 1);
    }
    private void Awake()
    {
        InstantiateAllSettings();
    }
    public void SetMaster(Single volume)
    {
        float AudioVolume = Mathf.Lerp(-80, 5, volume);
        Master.SetFloat("MasterVolume", AudioVolume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
    public void SetMusic(Single volume)
    {
        float AudioVolume = Mathf.Lerp(-80, 5, volume);
        Master.SetFloat("MusicVolume", AudioVolume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public void SetSFX(Single volume)
    {
        float AudioVolume = Mathf.Lerp(-80, 5, volume);
        Master.SetFloat("SFXVolume", AudioVolume);
        PlayerPrefs.SetFloat("SFXVolume", volume);

    }
    public void SetAmbient(Single volume)
    {
        float AudioVolume = Mathf.Lerp(-80, 5, volume);
        Master.SetFloat("AmbientVolume", AudioVolume);
        PlayerPrefs.SetFloat("AmbientVolume", volume);
    }
    public void SetUI(Single volume)
    {
        float AudioVolume = Mathf.Lerp(-80, 5, volume);
        Master.SetFloat("UIVolume", AudioVolume);
        PlayerPrefs.SetFloat("UIVolume", volume);
    }
    public void SetVoice(Single volume)
    {
        float AudioVolume = Mathf.Lerp(-80, 5, volume);
        Master.SetFloat("VoiceVolume", AudioVolume);
        PlayerPrefs.SetFloat("VoiceVolume", volume);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Fullscreen = isFullscreen;
        Screen.fullScreen = Fullscreen;
        PlayerPrefs.SetInt("Fullscreen", Convert.ToInt32(Fullscreen));
    }
    public void SetResolution(int index)
    {
        ResolutionIndex = index;
        PlayerPrefs.SetInt("ResolutionIndex", index);
        switch (index)
        {
            case 0:
                Screen.SetResolution(3840, 2160, Fullscreen);
                break;
            case 1:
                Screen.SetResolution(1920, 1080, Fullscreen);
                break;
            case 2:
                Screen.SetResolution(1600, 900, Fullscreen);
                break;
            case 3:
                Screen.SetResolution(1366, 768, Fullscreen);
                break;
            case 4:
                Screen.SetResolution(1280, 720, Fullscreen);
                break;
            case 5:
                Screen.SetResolution(1024, 576, Fullscreen);
                break;
            case 6:
                Screen.SetResolution(854, 480, Fullscreen);
                break;
            default:
                Screen.SetResolution(1920, 1080, Fullscreen);
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }



}
