using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[CustomEditor(typeof(SettingsManager))]
public class SettingsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var target = (SettingsManager)base.target;
        if (GUILayout.Button("Instantiate All Settings"))
        {
            target.InstantiateAllSettings();
        }
        if (GUILayout.Button("Create DropDown Elements"))
        {
            target.CreateDropDownElements();
        }
    }
}
enum TabSelected
{
    Game,
    Graphics,
    Audio,
    Other
}
public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    private TabSelected CurrentTab = TabSelected.Game;

    public AudioSource UISound;

    public AudioMixer Master;

    [SerializeField]
    private GameObject GameTab;
    [SerializeField]
    private GameObject GraphicsTab;
    [SerializeField]
    private GameObject AudioTab;
    [SerializeField]
    private GameObject OtherTab;

    [SerializeField]
    private GameObject GameTabObject, GraphicsTabObject, AudioTabObject, OtherTabObject;
    [SerializeField]
    private GameObject FirstSelectedGame, FirstSelectedGraphics, FirstSelectedAudio, FirstSelectedOther;

    [SerializeField]
    private Color ColorSelected, ColorUnselected;
    public int ResolutionIndex = 0;


    [Header("UI For Audio")]
    public Slider MasterSldier;
    public Slider MusicSlider;
    public Slider VoiceSlider;
    public Slider SFXSlider;
    public Slider UISlider;

    [Header("Variables for Graphics")]

    [Header("UI for Graphics")]
    public Toggle VSyncToggle;
    public Toggle AntiAliasingToggle;
    public Toggle BloomToggle;
    public Toggle ShadowsToggle;

    public TMP_Dropdown ResolutionDropdown;
    public TMP_Dropdown FullscreenModeDropDown;
    public TMP_Dropdown GraphicsDropdown;
    public TMP_Dropdown LightingDropdown;
    public TMP_Dropdown TextureDropdown;
    public TMP_Dropdown ShadowDropdown;
    

    [SerializeField] private InputActionReference Next, Previous, scroll;
    // Start is called before the first frame update
    private void Start()
    {
        SwitchTab((int)CurrentTab);
    }

    private void OnEnable()
    {
        Next.action.performed += OnNext;
        Previous.action.performed += OnPrevious;
        scroll.action.performed += OnScroll;
    }

    private void OnScroll(InputAction.CallbackContext obj)
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnPrevious(InputAction.CallbackContext obj)
    {
        int tabIndex = (int)CurrentTab - 1;
        if (tabIndex < 0)
        {
            tabIndex = Enum.GetNames(typeof(TabSelected)).Length - 1;
        }
        SwitchTab((int)tabIndex);
    }

    private void OnNext(InputAction.CallbackContext obj)
    {
        int tabIndex = (int)CurrentTab + 1;
        if (tabIndex >= Enum.GetNames(typeof(TabSelected)).Length)
        {
            tabIndex = 0;
        }
        SwitchTab((int)tabIndex);
    }
    public void InstantiateAllSettings()
    {
        MasterSldier.value = PlayerPrefs.GetFloat("MasterVolume", 0.7f);
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.7f);
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.7f);
        UISlider.value = PlayerPrefs.GetFloat("UIVolume", 0.7f);
        VoiceSlider.value = PlayerPrefs.GetFloat("VoiceVolume", 0.7f);
        Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("Fullscreen", 1));
        ResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 1);


    }
    public void CreateDropDownElements()
    {
        FullscreenModeDropDown.ClearOptions();
        ResolutionDropdown.ClearOptions();
        ShadowDropdown.ClearOptions();
        GraphicsDropdown.ClearOptions();
        LightingDropdown.ClearOptions();
        TextureDropdown.ClearOptions();
        ShadowDropdown.ClearOptions();

        var FullscreenOptions = new List<string>
        {
            "Fullscreen",
            "Borderless",
            "Windowed"
        };
        var ResolutionOptions = new List<string>
        {
            "3840 x 2160",
            "1920 x 1080",
            "1600 x 900",
            "1366 x 768",
            "1280 x 720",
            "1024 x 576",
            "854 x 480"
        };
        var ShadowOptions = new List<string>
        {
            "Low",
            "Medium",
            "High",
        };
        var LightingOptions = new List<string>
        {
            "Low",
            "Medium",
            "High",
            "Ultra",
        };
        var GraphicsPreset = new List<string>
        {
            "Low",
            "Medium",
            "High",
            "Ultra",
            "Plus-Ultra"
        };
        var TextureOptions = new List<string>
        {
            "Low",
            "Medium",
            "High",
        };
        ShadowDropdown.AddOptions(ShadowOptions);
        GraphicsDropdown.AddOptions(GraphicsPreset);
        LightingDropdown.AddOptions(LightingOptions);
        TextureDropdown.AddOptions(TextureOptions);
        ResolutionDropdown.AddOptions(ResolutionOptions);
        FullscreenModeDropDown.AddOptions(FullscreenOptions);

    }
    public void SwitchTab(int tabIndex)
    {

        //enum changes
        CurrentTab = (TabSelected)tabIndex;
        GameTab.SetActive(CurrentTab == TabSelected.Game);
        GraphicsTab.SetActive(CurrentTab == TabSelected.Graphics);
        AudioTab.SetActive(CurrentTab == TabSelected.Audio);
        OtherTab.SetActive(CurrentTab == TabSelected.Other);

        //text changes
        GameTabObject.GetComponentInChildren<TextMeshProUGUI>().color = (CurrentTab != TabSelected.Game) ? ColorUnselected : ColorSelected;
        GraphicsTabObject.GetComponentInChildren<TextMeshProUGUI>().color = (CurrentTab != TabSelected.Graphics) ? ColorUnselected : ColorSelected;
        AudioTabObject.GetComponentInChildren<TextMeshProUGUI>().color = (CurrentTab != TabSelected.Audio) ? ColorUnselected : ColorSelected;
        OtherTabObject.GetComponentInChildren<TextMeshProUGUI>().color = (CurrentTab != TabSelected.Other) ? ColorUnselected : ColorSelected;

        //first selected changes
        switch (CurrentTab)
        {
            case TabSelected.Game:
                EventSystem.current.SetSelectedGameObject(FirstSelectedGame);
                break;
            case TabSelected.Graphics:
                EventSystem.current.SetSelectedGameObject(FirstSelectedGraphics);
                break;
            case TabSelected.Audio:
                EventSystem.current.SetSelectedGameObject(FirstSelectedAudio);
                break;
            case TabSelected.Other:
                EventSystem.current.SetSelectedGameObject(FirstSelectedOther);
                break;
        }
    }
    private void Awake()
    {
        InstantiateAllSettings();
        UISound = GetComponent<AudioSource>();
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
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", Convert.ToInt32(isFullscreen));
    }
    public void SetResolution(int index)
    {
        ResolutionIndex = index;
        
        PlayerPrefs.SetInt("ResolutionIndex", index);
        switch (index)
        {
            
            case 0:
                Screen.SetResolution(3840, 2160, Screen.fullScreen);
                break;
            case 1:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;
            case 2:
                Screen.SetResolution(1600, 900, Screen.fullScreen);
                break;
            case 3:
                Screen.SetResolution(1366, 768, Screen.fullScreen);
                break;
            case 4: 
                Screen.SetResolution(1280, 720, Screen.fullScreen);
                break;
            case 5:
                Screen.SetResolution(1024, 576, Screen.fullScreen);
                break;
            case 6:
                Screen.SetResolution(854, 480, Screen.fullScreen);
                break;
            default:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;
        }
    }



}
