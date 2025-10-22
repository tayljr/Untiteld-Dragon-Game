using System.Collections;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public AnimationCurve knockbackCurve;
    public float knockbackStrength = 5f;
    public float knockbackDuration = 0.5f;
    private float knockbackTimer;
    private void OnEnable()
    {
        HealthBase.OnDamage += HealthBase_OnDamage;
    }
    private void OnDisable()
    {
        HealthBase.OnDamage -= HealthBase_OnDamage;
    }
    private void OnDestroy()
    {
        HealthBase.OnDamage -= HealthBase_OnDamage;
    }
    private void HealthBase_OnDamage(float damage, string tag)
    {
        if (tag == "Enemy")
        {
            StartCoroutine(KnockbackCorutine(damage, tag));
        }
    }

    IEnumerator KnockbackCorutine(float damage, string tag)
    {
        //enemy knockback logic
        if (tag == "Enemy")
        {
            var Charcontroller = GetComponent<CharacterController>();
            knockbackTimer = knockbackDuration;
            while (knockbackTimer > 0)
            {
                float curveKnockback = knockbackCurve.Evaluate(knockbackDuration / knockbackTimer);
                Vector3 knockbackDirection = -transform.forward;
                Charcontroller.Move(knockbackDirection * knockbackStrength * Time.deltaTime);
                knockbackTimer -= Time.deltaTime;
                yield return null;
            }
        }
            


        //player knockback logic
    }
}
