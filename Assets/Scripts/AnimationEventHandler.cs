using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{

    private EnemyCombatBase combat;
    private void Start()
    {
        combat = GetComponentInParent<EnemyCombatBase>();
    }
    public void HurtBoxOn()
    {
        combat.hurtBox.enabled = true;
    }
    public void HurtBoxOff()
    {
        combat.hurtBox.enabled = false;
    }
}
