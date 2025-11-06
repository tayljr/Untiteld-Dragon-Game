using System;
using System.Collections.Generic;
using UnityEngine;

public class FetchQuestBase : QuestBase
{
    //todo pick up base??
    public List<PickUpBase> pickUps;
    private List<PickUpBase> hasPickedups = new List<PickUpBase>();

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
            hasPickedups.Add(pickUpBase);

            if (hasPickedups.Count == pickUps.Count)
            {
                FinishedQuest();
            }
        }    
    }
}
