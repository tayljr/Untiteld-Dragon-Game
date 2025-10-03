using System;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class UpgradeManager : MonoBehaviour
{

    public List<string> activeUpgrades= new List<string>();

    private static UpgradeManager _instance;
    public static UpgradeManager instance { get { return _instance; } }
    
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
        
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void AddActiveUpgrade(UpgradeMonoBehaviour activeUpgrade)
    {
        if (!activeUpgrades.Contains(activeUpgrade.GetType().Name))
        {
            activeUpgrades.Add(activeUpgrade.GetType().Name);   
        }
    }
    /*
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        // Debug.unityLogger.Log("Scene Loaded");
        //for each upgrade in the list,
        foreach (string upgrade in activeUpgrades)
        {
            //get the name of the upgrade
            //string name = upgrade.GetType().Name;
            //find a script of the same name on the player
            UpgradeMonoBehaviour _upgrade = GameManager.instance._player.GetComponent(nameof(upgrade)) as UpgradeMonoBehaviour;
            //enable that script if it exists
            if (_upgrade != null) _upgrade.enabled = true;
        }
    }
    */
    private void Start()
    {
        
    }
}
