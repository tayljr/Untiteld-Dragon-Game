using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public Slider healthBar;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        HealthBase.OnDeath += HealthBase_OnDeath;
        HealthBase.OnHeal += HealthBase_OnHeal;
        HealthBase.OnDamage += HealthBase_OnDamage;
    }

    private void HealthBase_OnHeal(float heal, string tag)
    {
        throw new System.NotImplementedException();
    }

    private void HealthBase_OnDamage(float damage, string tag)
    {
        if (tag == "Player")
        {
            healthBar.value -= damage;
        }
    }

    private void HealthBase_OnDeath(string tag)
    {
        
    }

    private void OnDisable()
    {
        
    }
}
