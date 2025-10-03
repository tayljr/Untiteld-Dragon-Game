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
    }

    public void AddActiveUpgrade(UpgradeMonoBehaviour activeUpgrade)
    {
        if (!activeUpgrades.Contains(activeUpgrade.GetType().Name))
        {
            activeUpgrades.Add(activeUpgrade.GetType().Name);   
        }
    }
    
    private void Start()
    {
        
    }
}
