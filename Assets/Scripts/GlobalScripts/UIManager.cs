using UnityEngine;

public class UIManager : MonoBehaviour, IPauseable
{
    public bool IngameMenuOpen = false;
    public GameObject IngameMenu;

    public bool HUDOn = false;
    public GameObject HUD;

    public bool PauseMenuOpen = false;
    public GameObject pauseMenu;

    private void Update()
    {
        if (IngameMenu != null && IngameMenu)
        {
            IngameMenu.SetActive(IngameMenuOpen);
        }
        if (HUD != null && HUD)
        {
            HUD.SetActive(HUDOn);
        }

    }
    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(PauseMenuOpen);
    }
    public void OnPause()
    {
        PauseMenuOpen = true;
        Cursor.lockState = CursorLockMode.None;
        TogglePauseMenu();
    }
    public void OnResume()
    {
        PauseMenuOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
        TogglePauseMenu();
    }
}
