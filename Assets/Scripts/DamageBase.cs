using UnityEngine;
using System.Collections.Generic;

public enum DamageType{ 
    dps, 
    impact, 
}

public class DamageBase : MonoBehaviour
{
    [SerializeField]
    public string ignoreTag = "Player";

    public DamageType damageType = DamageType.impact;

    public float damage = 1f;

    private List<HealthBase> healthList = new List<HealthBase>();

    public Collider hurtBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hurtBox = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hurtBox.enabled == false)
        {
            healthList.Clear();
        }

        if(damageType == DamageType.dps)
        {
            foreach(HealthBase health in healthList)
            {
                health.Damage(damage * Time.deltaTime);
            }
        }
        else
        {
            foreach (HealthBase health in healthList)
            {
                health.Damage(damage);
            }
            healthList.Clear();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        //print("touching" + collision.gameObject.name);
        GameObject hitObject = collision.gameObject;
        HealthBase health = hitObject.GetComponent<HealthBase>();
        if (health != null && !healthList.Contains(health) && hitObject.tag != ignoreTag)
        {
            healthList.Add(health);
        }
    }

    private void OnTriggerExit(Collider collision) 
    {
        HealthBase health = collision.gameObject.GetComponent<HealthBase>();
        if (health != null && healthList.Contains(health))
        {
            healthList.Remove(health);
        }
    }
}
