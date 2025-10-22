using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatZone : MonoBehaviour
{
    public int killsInArea = 0;
    public int killsNeeded = 0;

    public List<GameObject> objectsInTrigger = new List<GameObject>();

    public GameObject wall;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Enemy"))
        {
            
            if (objectsInTrigger.Contains(other.gameObject))
            {
                objectsInTrigger.Add(other.gameObject);
                Debug.Log("Added {other.gameObject.name} to the list.");
            }
        }
        else
        {
            return;
        }
    }

    
    private void OnTriggerExit(Collider other)
    {
        if (objectsInTrigger.Contains(other.gameObject))
        {
            objectsInTrigger.Remove(other.gameObject);
            Debug.Log("Removed {other.gameObject.name} from the list.");
        }
    }

    
    public void RegisterKill()
    {
        killsInArea++;
        Debug.Log("Kill registered in area! Total kills: " + killsInArea);
        //Add Ui at some point
    }

    public void Update()
    {
        if (killsInArea >= killsNeeded)
        {
            //Replace with animation
            Destroy(wall);
        }
    }
}
