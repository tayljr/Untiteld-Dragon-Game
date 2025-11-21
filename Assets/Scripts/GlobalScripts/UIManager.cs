using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IPauseable
{
    public bool StartMenu = true;

    public bool HUDOn = false;
    public GameObject HUD;

    public bool PauseMenuOpen = false;
    public GameObject pauseMenu;

    public bool SettingsMenuOpen = false;
    public GameObject Music;

    [Header("FirstSelectedUI")]
    public GameObject firstSelectedPauseMenu;
    [Header("CurrentSelectedUI")]
    [SerializeField]
    private GameObject currentSelectedUI;

    private void Update()
    {
        currentSelectedUI = EventSystem.current.currentSelectedGameObject;
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
            Cursor.lockState = CursorLockMode.None;
        }
        Music.SetActive(!SettingsMenuOpen);
        
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
        
    }

    private void SceneManager_sceneUnloaded(Scene arg0)
    {
        if (arg0.name == "Settings")
        {
            SettingsMenuOpen = false;
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
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                Button button = FindAnyObjectByType<Button>();
                EventSystem.current.SetSelectedGameObject(button.gameObject);
            }
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
