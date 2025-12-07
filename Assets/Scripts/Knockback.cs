using System.Collections;
using UnityEngine;

public class Knockback : MonoBehaviour , IHit
{
    public AnimationCurve knockbackCurve;
    public float knockbackStrength = 5f;
    public float knockbackDuration = 0.5f;
    private float knockbackTimer;
    public void Hit(object args)
    {
        if (tag == "Enemy")
        {
            float damage = ((object[])args)[0] is float ? (float)((object[])args)[0] : 0f;
            string tag = ((object[])args)[1] is string ? (string)((object[])args)[1] : "";
            Vector3 direction = ((object[])args)[2] is Vector3 ? (Vector3)((object[])args)[2] : Vector3.forward;

            StartCoroutine(KnockbackCorutine(damage,direction, tag));
        }
    }

    IEnumerator KnockbackCorutine(float damage,Vector3 direction, string tag)
    {
        //enemy knockback logic
        if (tag == "Enemy")
        {
            var Charcontroller = GetComponent<CharacterController>();
            knockbackTimer = knockbackDuration;
            while (knockbackTimer > 0)
            {
                float curveKnockback = knockbackCurve.Evaluate(knockbackDuration / knockbackTimer);
                Vector3 knockbackDirection = direction;
                float convertedStrength = knockbackStrength * damage;
                Charcontroller.Move(knockbackDirection * convertedStrength * Time.deltaTime);
                knockbackTimer -= Time.deltaTime;
                yield return null;
            }
        }
            


        //player knockback logic
    }
}
