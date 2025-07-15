using UnityEngine;
using System.Collections;
public class HealthBase : MonoBehaviour
{
    public float maxHealth = 10f;
    public float health = 10f;
    public float healthPercent = 100f;

    public float iFrameTime = 0.25f;

    public bool invincible = false;

    public bool isDead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth * (healthPercent / 100);
        invincible = false;
        HealthCheck();
    }

    public void Damage(float amount)
    {
        if (!invincible)
        {
            health -= amount;
            StartCoroutine(IFrames());
            HealthCheck();
        }
    }

    public void DamagePercent(float percentage)
    {
        if (!invincible)
        {
            health -= maxHealth * (percentage / 100);
            StartCoroutine(IFrames());
            HealthCheck();
        }
    }

    IEnumerator IFrames()
    {
        invincible = true;
        yield return new WaitForSeconds(iFrameTime);
        invincible = false;
    }

    public void Heal(float amount)
    {
        health += amount;

        HealthCheck();
    }

    public void HealPercent(float percentage)
    {
        health += maxHealth * (percentage / 100);

        HealthCheck();
    }

    private void HealthCheck()
    {
        if (health <= 0)
        {
            health = 0;
            isDead = true;
            Debug.Log("Dead");
        }
        else if(health > maxHealth)
        {
            health = maxHealth;
            isDead = false;
        }
        else
        {
            isDead = false;
        }
        healthPercent = (health / maxHealth) * 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
