using UnityEngine;

public class QuestPickUp : PickUpBase
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            PickUpItem(this);

           

            Destroy(gameObject);
        }
    }
}
