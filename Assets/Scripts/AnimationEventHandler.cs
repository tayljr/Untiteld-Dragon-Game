using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{

    private EnemyCombatBase combat;
    public void Attack()
    {
        gameObject.BroadcastMessage("StartAttack");
    }
}
