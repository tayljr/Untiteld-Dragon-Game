using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IPauseable
{
    public GameObject _player;
    private static GameManager _instance;
    public static GameManager instance {  get { return _instance; } }  
    
    public bool gameOver = false;

    public bool gameStartMenu = true;

    public bool isPaused = false;
    
    public bool inDialogue = false;

    private PlayerInput _playerInput;

    [Header("Audio cause idk")]
    public AudioMixer Master;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    void Start()
    {
     _playerInput = GetComponent<PlayerInput>();   
        StartCoroutine(FindPlayer(player =>
        {
            Debug.Log("found Player" + player.name);
        }));
    }
    public void OnGameOver()
    {
        gameOver = true;
        OnPause();
    }
    public void GameAwake()
    {
        InstantiateAllSettings();

    }
    public void InstantiateAllSettings()
    {
        //audio settings
        Master.SetFloat("Master", PlayerPrefs.GetFloat("MasterVolume", 1f));
        Master.SetFloat("Music", PlayerPrefs.GetFloat("MusicVolume", 0.5f));
        Master.SetFloat("SFX", PlayerPrefs.GetFloat("SFXVolume", 0.6f));
        Master.SetFloat("UI", PlayerPrefs.GetFloat("UIVolume", 1f));
        Master.SetFloat("Voice", PlayerPrefs.GetFloat("VoiceVolume", 1f));
        //graphics settings
        //Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("Fullscreen", 1));
        //ResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 1);
        //FrameRateDropdown.value = PlayerPrefs.GetInt("FrameRate", 3);
        //AntiAliasingDropdown.value = PlayerPrefs.GetInt("AntiAliasing", 1);
        //TextureDropdown.value = PlayerPrefs.GetInt("TextureQuality", 1);
        //LightingDropdown.value = PlayerPrefs.GetInt("LightingQuality", 1);
        //ShadowDropdown.value = PlayerPrefs.GetInt("ShadowQuality", 1);
        //GraphicsDropdown.value = PlayerPrefs.GetInt("GraphicsPreset", 2);
        //VSyncToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("VSync", 1));
        //ShadowsToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("Shadows", 1));
        //BloomToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("Bloom", 1));
        ////game settings
        _player.GetComponent<PlayerController>().SetSensitivity(PlayerPrefs.GetFloat("LookSensitivity", 0.5f));

        //i dont know :(
    }
    public void OnPause()
    {
        isPaused = true;
        if (!gameStartMenu)
        {
            _player.SendMessage("OnPause");
            _playerInput.actions.FindActionMap("Player").Disable();
            _playerInput.actions.FindActionMap("UI").Enable();
        }
    }
    public void OnResume()
    {
        isPaused = false;
        if (!gameStartMenu)
        {
            _player.SendMessage("OnResume");
            _playerInput.actions.FindActionMap("UI").Disable();
            _playerInput.actions.FindActionMap("Player").Enable();
        }

    }
    public void OnMainMenu()
    {
        gameOver = false;
        gameStartMenu = true;
        OnResume();
    }
    public void GameStart()
    {
        gameStartMenu = false;
        gameObject.SendMessage("OnGameStart");
    }
    // Update is called once per frame
    void Update()
    {
            Time.timeScale = isPaused ? 0f : 1f ;
            
    }
    //im going to real chatGPT made this, its kinda peam!
    public IEnumerator FindPlayer(System.Action<GameObject> onFound)
    {
        // Make a container for this piece of shit so you can hold something; otherwise it's a slippery fuck
        GameObject player = null;

        // Now we make a bounty for this fucker
        while (player == null)
        {
            // If you find that fucker, put that fucker in that little fucking container you just made
            player = GameObject.FindWithTag("Player");

            if (player == null)
            {
                // Now wait, otherwise this little function gonna blow up your funny little CPU
                yield return null;
            }
        }

        // Store the found bastard so other scripts can use it
        _player = player;

        // CALL TO THE WORLD THAT YOU JUST CAUGHT THIS SON OF A BITCH AND IF 
        // ANYBODY WANTS THIS FUCKER YOU CAN HAVE HIM!!!
        onFound?.Invoke(player);
    }

}
