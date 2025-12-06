using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;

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
#endif
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

    public AudioSource StrongSound;
    public AudioSource WeakSound;
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

    [Header("UI For Game")]
    public Slider SensitivitySlider;


    [Header("UI For Other")]


    [Header("UI For Audio")]
    public Slider MasterSldier;
    public Slider MusicSlider;
    public Slider VoiceSlider;
    public Slider SFXSlider;
    public Slider UISlider;

    [Header("Variables for Graphics")]

    [Header("UI for Graphics")]
    public Toggle VSyncToggle;
    public Toggle BloomToggle;
    public Toggle ShadowsToggle;

    public TMP_Dropdown ResolutionDropdown;
    public TMP_Dropdown FrameRateDropdown;
    public TMP_Dropdown AntiAliasingDropdown;
    public TMP_Dropdown FullscreenModeDropDown;
    public TMP_Dropdown GraphicsDropdown;
    public TMP_Dropdown LightingDropdown;
    public TMP_Dropdown TextureDropdown;
    public TMP_Dropdown ShadowDropdown;

    private GameObject lastselected;

    

    [SerializeField] private InputActionReference Next, Previous, scroll, Navigate;
    // Start is called before the first frame update
    private void Start()
    {
        SwitchTab((int)CurrentTab);
        SetAllValues();
    }

    private void OnEnable()
    {
        Next.action.performed += OnNext;
        Previous.action.performed += OnPrevious;
        scroll.action.performed += OnScroll;
        Navigate.action.performed += OnNavigate;
        
    }
    private void Update()
    {
        var current = EventSystem.current.currentSelectedGameObject;
        if (current != lastselected && current != null)
        {
            lastselected = current;
            try
            {
                PlayUISound(current.tag);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e + $"{current} tryed to play sound");
            }
        }
    }
    private void OnNavigate(InputAction.CallbackContext obj)
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
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
    }

    private void OnScroll(InputAction.CallbackContext obj)
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnPrevious(InputAction.CallbackContext obj)
    {
        PlayUISound("Strong");
        int tabIndex = (int)CurrentTab - 1;
        if (tabIndex < 0)
        {
            tabIndex = Enum.GetNames(typeof(TabSelected)).Length - 1;
        }
        SwitchTab((int)tabIndex);
    }

    private void OnNext(InputAction.CallbackContext obj)
    {
        PlayUISound("Strong");
        int tabIndex = (int)CurrentTab + 1;
        if (tabIndex >= Enum.GetNames(typeof(TabSelected)).Length)
        {
            tabIndex = 0;
        }
        SwitchTab((int)tabIndex);
    }
    public void InstantiateAllSettings()
    {
        //audio settings
        MasterSldier.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.6f);
        UISlider.value = PlayerPrefs.GetFloat("UIVolume", 1f);
        VoiceSlider.value = PlayerPrefs.GetFloat("VoiceVolume", 1f);
        //graphics settings
        Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("Fullscreen", 1));
        ResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 1);
        FrameRateDropdown.value = PlayerPrefs.GetInt("FrameRate", 3);
        AntiAliasingDropdown.value = PlayerPrefs.GetInt("AntiAliasing", 1);
        TextureDropdown.value = PlayerPrefs.GetInt("TextureQuality", 1);
        LightingDropdown.value = PlayerPrefs.GetInt("LightingQuality", 1);
        ShadowDropdown.value = PlayerPrefs.GetInt("ShadowQuality", 1);
        GraphicsDropdown.value = PlayerPrefs.GetInt("GraphicsPreset", 2);
        VSyncToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("VSync", 1));
        ShadowsToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("Shadows", 1));
        BloomToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("Bloom", 1));
        //game settings
        SensitivitySlider.value = PlayerPrefs.GetFloat("LookSensitivity", 0.5f);
    }
    public void SetAllValues()
    {
        Master.SetFloat("MasterVolume", Mathf.Lerp(-80, 10, MasterSldier.value));
        Master.SetFloat("MusicVolume", Mathf.Lerp(-80, 10, MusicSlider.value));
        Master.SetFloat("SFXVolume", Mathf.Lerp(-80, 10, SFXSlider.value));
        Master.SetFloat("UIVolume", Mathf.Lerp(-80, 10, UISlider.value));
        Master.SetFloat("VoiceVolume", Mathf.Lerp(-80, 10, VoiceSlider.value));


    }
    public void ApplyGraphicsPreset(string presetIndex)
    {
        switch (presetIndex)
        {
            case "Low":
                FrameRateDropdown.value = 1;
                ShadowDropdown.value = 0;
                TextureDropdown.value = 0;
                LightingDropdown.value = 0;
                AntiAliasingDropdown.value = 0;
                BloomToggle.isOn = false;
                ShadowsToggle.isOn = false;
                break;
            case "Medium":
                ShadowDropdown.value = 1;
                TextureDropdown.value = 1;
                LightingDropdown.value = 1;
                AntiAliasingDropdown.value = 1;
                BloomToggle.isOn = true;
                ShadowsToggle.isOn = true;
                break;
            case "High":
                FrameRateDropdown.value = 4;
                ShadowDropdown.value = 2;
                TextureDropdown.value = 2;
                LightingDropdown.value = 2;
                AntiAliasingDropdown.value = 2;
                BloomToggle.isOn = true;
                ShadowsToggle.isOn = true;
                break;
        }
    }
    public void CreateDropDownElements()
    {
        FullscreenModeDropDown.ClearOptions();
        AntiAliasingDropdown.ClearOptions();
        FrameRateDropdown.ClearOptions();
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
        var AntiAliasingOptions = new List<string>
        {
            "Off",
            "2x",
            "4x",
            "8x"
        };
        var FrameRateOptions = new List<string>
        {
            "5 FPS" /* yea this is kinda just for the lol's */,
            "15 FPS"/* yea this is kinda just for the lol's */,
            "30 FPS",
            "60 FPS",
            "120 FPS",
            "144 FPS",
            "Unlimited"
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
        };
        var GraphicsPreset = new List<string>
        {
            "Low",
            "Medium",
            "High",
        };

        var TextureOptions = new List<string>
        {
            "Low",
            "Medium",
            "High",
        };
        ShadowDropdown.AddOptions(ShadowOptions);
        GraphicsDropdown.AddOptions(GraphicsPreset);
        FrameRateDropdown.AddOptions(FrameRateOptions);
        AntiAliasingDropdown.AddOptions(AntiAliasingOptions);
        LightingDropdown.AddOptions(LightingOptions);
        TextureDropdown.AddOptions(TextureOptions);
        ResolutionDropdown.AddOptions(ResolutionOptions);
        FullscreenModeDropDown.AddOptions(FullscreenOptions);

    }
    public void PlayUISound(string tag)
    {
        if (tag == "Weak")
            WeakSound.PlayOneShot(WeakSound.clip);
        else if (tag == "Strong")
            StrongSound.PlayOneShot(StrongSound.clip);

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
    }
    public void SetMaster(Single volume)
    {
        float AudioVolume = Mathf.Lerp(-80, 10, volume);
        Master.SetFloat("MasterVolume", AudioVolume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
    public void SetMusic(Single volume)
    {
        float AudioVolume = Mathf.Lerp(-80, 10, volume);
        Master.SetFloat("MusicVolume", AudioVolume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public void SetSFX(Single volume)
    {
        float AudioVolume = Mathf.Lerp(-80, 10, volume);
        Master.SetFloat("SFXVolume", AudioVolume);
        PlayerPrefs.SetFloat("SFXVolume", volume);

    }
    public void SetUI(Single volume)
    {
        float AudioVolume = Mathf.Lerp(-80, 10, volume);
        Master.SetFloat("UIVolume", AudioVolume);
        PlayerPrefs.SetFloat("UIVolume", volume);
    }
    public void SetVoice(Single volume)
    {
        float AudioVolume = Mathf.Lerp(-80, 10, volume);
        Master.SetFloat("VoiceVolume", AudioVolume);
        PlayerPrefs.SetFloat("VoiceVolume", volume);
    }
    public void SetFullscreen(int fullscreenMode)
    {
        PlayerPrefs.SetInt("Fullscreen", fullscreenMode);
        switch (fullscreenMode)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }

    }
    public void SetGraphicsPreset(int presetIndex)
    {
        PlayerPrefs.SetInt("GraphicsPreset", presetIndex);
        switch (presetIndex)
        {
            case 0:
                ApplyGraphicsPreset("Low");
                break;
            case 1:
                ApplyGraphicsPreset("Medium");
                break;
            case 2:
                ApplyGraphicsPreset("High");
                break;
        }
    }
    public void SetAntiAliasing(int aaIndex)
    {
        PlayerPrefs.SetInt("AntiAliasing", aaIndex);
        switch (aaIndex)
        {
            case 0:
                QualitySettings.antiAliasing = 0;
                break;
            case 1:
                QualitySettings.antiAliasing = 2;
                break;
            case 2:
                QualitySettings.antiAliasing = 4;
                break;
            case 3:
                QualitySettings.antiAliasing = 8;
                break;
        }
    }
    public void SetShadowQuality(int shadowIndex)
    {
        PlayerPrefs.SetInt("ShadowQuality", shadowIndex);
        switch (shadowIndex)
        {
            case 0:
                QualitySettings.shadowResolution = ShadowResolution.Low;
                QualitySettings.shadowCascades = 0;
                break;
            case 1:
                QualitySettings.shadowResolution = ShadowResolution.Medium;
                QualitySettings.shadowCascades = 2;
                break;
            case 2:
                QualitySettings.shadowResolution = ShadowResolution.High;
                QualitySettings.shadowCascades = 4;
                break;
        }
    }
    public void SetTextureQuality(int textureIndex)
    {
        PlayerPrefs.SetInt("TextureQuality", textureIndex);
        switch (textureIndex)
        {
            case 0:
                QualitySettings.globalTextureMipmapLimit = 2;
                break;
            case 1:
                QualitySettings.globalTextureMipmapLimit = 1;
                break;
            case 2:
                QualitySettings.globalTextureMipmapLimit = 0;
                break;
        }
    }
    public void SetLightingQuality(int lightingIndex)
    {
        PlayerPrefs.SetInt("LightingQuality", lightingIndex);
        switch (lightingIndex)
        {
            case 0:
                QualitySettings.pixelLightCount = 1;
                QualitySettings.shadowDistance = 15;
                break;
            case 1:
                QualitySettings.pixelLightCount = 2;
                QualitySettings.shadowDistance = 30;
                break;
            case 2:
                QualitySettings.pixelLightCount = 4;
                QualitySettings.shadowDistance = 60;
                break;
        }
    }
    public void SetBloom(bool isBloom)
    {
        PlayerPrefs.SetInt("Bloom", Convert.ToInt32(isBloom));
        //insert bloom toggle code here
    }
    public void SetShadows(bool isShadows)
    {
        PlayerPrefs.SetInt("Shadows", Convert.ToInt32(isShadows));
        QualitySettings.shadows = isShadows ? ShadowQuality.All : ShadowQuality.Disable;
    }
    public void SetFramerate(int framerate)
    {
        PlayerPrefs.SetInt("FrameRate", framerate);
        switch (framerate)
        {
            case 0:
                Application.targetFrameRate = 5;
                break;
            case 1:
                Application.targetFrameRate = 15;
                break;
            case 2:
                Application.targetFrameRate = 30;
                break;
            case 3:
                Application.targetFrameRate = 60;
                break;
            case 4:
                Application.targetFrameRate = 120;
                break;
            case 5:
                Application.targetFrameRate = 144;
                break;
            case 6:
                Application.targetFrameRate = -1;
                break;
        }
        }    public void SetResolution(int index)
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
    public void SetVSync(bool isVSync)
    {
        QualitySettings.vSyncCount = isVSync ? 1 : 0;
        PlayerPrefs.SetInt("VSync", Convert.ToInt32(isVSync));
    }
    public void SetSensitivity(float value)
    {
        PlayerPrefs.SetFloat("LookSensitivity", value);
    }

}
