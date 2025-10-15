using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public Slider healthBar;
    private HealthBase playerHealth;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBase>();
        healthBar.value = playerHealth.health;

    }
}
