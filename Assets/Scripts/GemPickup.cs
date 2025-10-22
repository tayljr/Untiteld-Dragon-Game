using UnityEngine;

public class GemPickup : MonoBehaviour
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
           
            GemManager.Instance.AddGems(gemValue);

            Destroy(gameObject);
        }
    }
}

