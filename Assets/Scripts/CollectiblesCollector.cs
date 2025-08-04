using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class CollectiblesCollector : MonoBehaviour
{

    private int Collectibles = 0;

    public TextMeshProUGUI collectibleText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Collectible")
        {
            Collectibles++;
            collectibleText.text = "Collectibles: " + Collectibles.ToString();
            Debug.Log(Collectibles);
            Debug.Log("TEST");
            Destroy(other.gameObject);
        }
    }
}