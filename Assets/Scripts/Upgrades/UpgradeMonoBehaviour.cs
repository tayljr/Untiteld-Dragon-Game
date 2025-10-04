using System;
using UnityEngine;

public class UpgradeMonoBehaviour : MonoBehaviour
{
    private void Awake()
    {
        //when the scene loads, check the upgrade manger to see if this upgrade was active
        if (UpgradeManager.instance != null && UpgradeManager.instance.activeUpgrades.Contains(GetType().Name))
        {
            enabled = true;
        }
    }

    private void OnDestroy()
    {
        //when the scene unloads, if this upgrade is active, tell the upgrade manager to remember that
        if(enabled)
        {
            UpgradeManager.instance.AddActiveUpgrade(this);
        }
    }
}
