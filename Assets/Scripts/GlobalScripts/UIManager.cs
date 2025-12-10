using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IPauseable
{

    [SerializeField] private GameObject DebugSelected;

    public bool StartMenu = true;

    public bool HUDOn = false;
    public GameObject HUD;

    public bool PauseMenuOpen = false;
    public GameObject pauseMenu;

    public bool SettingsMenuOpen = false;
    public GameObject Music;

    [SerializeField] private InputActionReference ExitCancelAction;


    [Header("FirstSelectedUI")]
    public GameObject firstSelectedPauseMenu;



    private void Update()
    {
        DebugSelected = EventSystem.current.currentSelectedGameObject;
        if (!StartMenu)
        {
            if (HUD != null && HUD)
            {
                HUD.SetActive(HUDOn);
            }
        }
        else
        {
            pauseMenu.SetActive(false);
            HUD.SetActive(false);
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(GameObject.Find("Start"));
            }
            Cursor.lockState = CursorLockMode.None;
        }
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
        
        ExitCancelAction.action.performed += OnExit;
    }

    private void OnExit(InputAction.CallbackContext obj)
    {
        if(SettingsMenuOpen)
        {
            SceneManager.UnloadSceneAsync("Settings");
        }
        else if (!StartMenu && !SettingsMenuOpen)
        {
            if (PauseMenuOpen)
            {
                SendMessage("OnResume");
            }
            else
            {
                SendMessage("OnPause");
            }
        }
    }

    private void SceneManager_sceneUnloaded(Scene arg0)
    {
        if (arg0.name == "Settings")
        {
            SettingsMenuOpen = false;
            Cursor.lockState = CursorLockMode.None;
            SendMessage("OnResume");
        }
    }
    
    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (arg0.name == "MainMenu")
        {
            OnMainMenu();
            SettingsMenuOpen = false;
            
            //just to be sure the settings menu is closed
            if (SceneManager.GetSceneByName("settings").isLoaded)
            {
                SceneManager.UnloadSceneAsync("Settings");
            }
        }
        if (arg0.name == "Tutorial")
        {
            SendMessage("OnGameStart");
            HUDOn = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (arg0.name == "Settings")
        {
            SettingsMenuOpen = true;
            Cursor.lockState = CursorLockMode.None;
            SendMessage("OnPause");
        }
    }

    public void OnGameStart()
    {
        StartMenu = false;

    }
    public void OnMainMenu()
    {
        StartMenu = true;
        PauseMenuOpen = false;
        HUDOn = false;
        EventSystem.current.SetSelectedGameObject(GameObject.Find("Start"));
    }
    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(PauseMenuOpen);
    }
    public void OnPause()
    {
        PauseMenuOpen = true;
        HUDOn = false;
        Cursor.lockState = CursorLockMode.None;
        TogglePauseMenu();
        if (EventSystem.current.currentSelectedGameObject == null && SceneManager.GetActiveScene().name != "Settings")
        {
            EventSystem.current.SetSelectedGameObject(firstSelectedPauseMenu);
        }
    }
    public void OnResume()
    {
        PauseMenuOpen = false;
        HUDOn = true;
        Cursor.lockState = CursorLockMode.Locked;
        TogglePauseMenu();
    }
}
