using System;
using System.Collections.Generic;
using UnityEngine;

public class FetchQuestBase : QuestBase
{
    public List<PickUpBase> pickUps;
    private List<PickUpBase> hasPickedUps = new List<PickUpBase>();

    public void OnEnable()
    {
        foreach(PickUpBase pickUp in pickUps)
        {
            pickUp.PickUpEvent += PickupOnpickUpEvent;
        }
    }

    public void OnDisable()
    {
        foreach (PickUpBase pickUp in pickUps)
        {
            pickUp.PickUpEvent -= PickupOnpickUpEvent;
        }
    }

    private void PickupOnpickUpEvent(PickUpBase pickUpBase)
    {
        if (currentState == QuestState.doing)
        {
            hasPickedUps.Add(pickUpBase);

            if (hasPickedUps.Count == pickUps.Count)
            {
                FinishedQuest();
            }
        }    
    }
}
