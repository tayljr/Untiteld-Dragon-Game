using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SceneUILock : MonoBehaviour
{

    public Selectable[] ObjectsToLockOnMainMenu;
    public Selectable[] ObjectsToLockOnSettings;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
    }

    private void SceneManager_sceneUnloaded(Scene arg0)
    {

        if (arg0.name == "Settings")
        {
            foreach (var item in ObjectsToLockOnMainMenu)
            {
                item.interactable = false;
            }
            foreach (var item in ObjectsToLockOnSettings)
            {
                item.interactable = true;
            }
        }

    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }
    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "MainMenu")
        {
            foreach (var item in ObjectsToLockOnMainMenu)
            {
                item.interactable = false;
            }
            foreach (var item in ObjectsToLockOnSettings)
            {
                item.interactable = true;
            }
        }
        if (arg0.name == "Settings")
        {
            foreach (var item in ObjectsToLockOnMainMenu)
            {
                item.interactable = true;
            }
            foreach (var item in ObjectsToLockOnSettings)
            {
                item.interactable = false;
            }
        }
    }

    // Update is called once per frame

}
