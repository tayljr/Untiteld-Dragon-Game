using System;
using UnityEngine;

public class UnlockUpgrade : MonoBehaviour
{
    [Tooltip("The name of the script to unlock. i.e. GlideUpgrade")]
    public string upgradeName = "GlideUpgrade";
    
    //event for things like tutorial or vfx to listen to
    public delegate void NewUpgradeDelegate(string name);
    public static event NewUpgradeDelegate OnNewUpgrade;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            if (other.gameObject.tag == "Player")
            {
                //find the script in the player that has the same name as the variable
                MonoBehaviour upgrade = other.GetComponent(upgradeName) as MonoBehaviour;
                if (upgrade != null)
                {
                    //if the script was found, then enable it
                    upgrade.enabled = true;
                }
                
                //send the event for anything visual etc.
                OnNewUpgrade?.Invoke(upgradeName);
                
                //deletes this object
                gameObject.SetActive(false);
            }
        }
    }
}
