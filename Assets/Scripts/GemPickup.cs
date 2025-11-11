using UnityEngine;

public class GemPickup : PickUpBase
{
    public int gemValue = 1;
    private AudioSource gemSound;
    private void Start()
    {
         gemSound = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other) 
    {
       
        if (other.CompareTag("Player"))
        {
            PickUpItem(this);
            
            GemManager.Instance.AddGems(gemValue);

            Destroy(gameObject);
        }
    }
}

