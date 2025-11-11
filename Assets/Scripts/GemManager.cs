using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GemManager : MonoBehaviour
{
    public delegate void GemDelegate(int amount);
    
    public static event GemDelegate OnGemAdded;
    
    public static GemManager Instance { get; private set; } 

    public TextMeshProUGUI gemCountText;
    [SerializeField]
    private int totalGems = 0;
    [SerializeField]
    private AudioSource gemSound;
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
        OnGemAdded?.Invoke(amount);
        totalGems += amount;
        UpdateGemUI();
        gemSound.Play();
        Debug.Log("Total Gems: " + totalGems);
    }

    private void UpdateGemUI()
    {
        if (gemCountText != null)
        {
            gemCountText.text = totalGems.ToString();
        }
    }
}
