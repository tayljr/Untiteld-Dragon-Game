using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class Waypointpath : MonoBehaviour
{
  public Transform GetWaypoint(int waypointIndex)
    {
        return transform.GetChild(waypointIndex);
    }

    public int GetNextWaypointIndex(int currentWayPointIndex)
    {
        int nextWaypointIndex = currentWayPointIndex + 1;

        if (nextWaypointIndex == transform.childCount)
        {
            nextWaypointIndex = 0;
        }

        return nextWaypointIndex;
    }


}
