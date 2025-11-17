using System.Collections.Generic;
using UnityEngine;

public class VisitQuestBase : QuestBase
{
    public Transform playerTransform;
    public List<Transform> visitLocations;
    public float visitDistance = 3f;
    private List<Transform> hasVisitLocations = new List<Transform>();
    
    
    public void OnEnable()
    {
        
    }

    public void OnDisable()
    {
    }

    void FixedUpdate()
    {
        if (currentState == QuestState.doing)
        {
            foreach (Transform location in visitLocations)
            {
                float distance = Vector3.Distance(playerTransform.position, location.position);

                if (distance <= visitDistance)
                {
                    hasVisitLocations.Add(location);
                }
                
            }
            if (visitLocations.Count == hasVisitLocations.Count)
            {
                FinishedQuest();
            }
        }    
    }
}
