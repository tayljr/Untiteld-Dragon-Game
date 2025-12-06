using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class CollectiblesCollector : MonoBehaviour
{
    public static CollectiblesCollector Instance { get; private set;  }

    public int Collectibles = 0;

    public int Keys = 0;

    public TextMeshProUGUI collectibleText;

    public TextMeshProUGUI keyText;

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        UpdateValues();
    }
    public void UpdateValues()
    {
        collectibleText.text = Collectibles.ToString();
        keyText.text = Keys.ToString();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void CollectTheCollectable(GameObject other)
    {
        if (other.transform.tag == "Collectible")
        {
            Collectibles++;
            Debug.Log("A");
            UpdateValues();
            Destroy(other.gameObject);
        }

        if (other.transform.tag == "Key")
        {
            //km.KeyCount++;
            Keys++;
            UpdateValues();
            Destroy(other.gameObject);
        }

    }
    
}