using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GemManager : MonoBehaviour
{
    public static GemManager Instance { get; private set; } 

    public TextMeshProUGUI gemCountText; 
    private int totalGems = 0;

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
    }

    private void Start()
    {
        UpdateGemUI(); 
    }

    public void AddGems(int amount)
    {
        totalGems += amount;
        UpdateGemUI();
        Debug.Log("Total Gems: " + totalGems);
    }

    private void UpdateGemUI()
    {
        if (gemCountText != null)
        {
            gemCountText.text = "Gems: " + totalGems.ToString();
        }
    }
}
