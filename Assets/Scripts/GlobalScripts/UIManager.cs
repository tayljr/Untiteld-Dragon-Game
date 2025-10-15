using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour, IPauseable
{
    public bool StartMenu = true;

    public bool HUDOn = false;
    public GameObject HUD;

    public bool PauseMenuOpen = false;
    public GameObject pauseMenu;

    public bool SettingsMenuOpen = false;
    public GameObject Music;
    private void Update()
    {
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
        if (arg0.name == "MainMenu")
        {
            OnMainMenu();
            SettingsMenuOpen = false;
            //just to be sure the settings menu is closed
            SceneManager.UnloadSceneAsync("Settings");
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
    }
    public void OnResume()
    {
        PauseMenuOpen = false;
        HUDOn = true;
        Cursor.lockState = CursorLockMode.Locked;
        TogglePauseMenu();
    }
}
