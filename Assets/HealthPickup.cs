using UnityEngine;

public class HealthPickup : PickUpBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("test");
        if (other.CompareTag("Player"))
        {
            PickUpItem(this);

            other.GetComponentInParent<HealthBase>().HealPercent(100);

            Destroy(gameObject);
        }
    }
}
