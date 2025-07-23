using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{

    private EnemyCombatBase combat;
    private void Start()
    {
        combat = GetComponentInParent<EnemyCombatBase>();
    }
    public void CallEvent(AnimationEventObj animationEvent) 
    { 

        
    }
}
