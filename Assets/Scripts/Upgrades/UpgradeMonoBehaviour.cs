using System;
using UnityEngine;

public class UpgradeMonoBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        if (UpgradeManager.instance.activeUpgrades.Contains(GetType().Name))
        {
            enabled = true;
        }
    }

    private void OnDestroy()
    {
        if(enabled)
        {
            UpgradeManager.instance.AddActiveUpgrade(this);
        }
    }
}
