using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class CollectablePickup : PickUpBase
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

            CollectiblesCollector.Instance.CollectTheCollectable(gameObject);

            Destroy(gameObject);
        }
    }
}
