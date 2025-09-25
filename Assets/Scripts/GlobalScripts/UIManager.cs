using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public bool IngameMenuOpen = false;
    public GameObject IngameMenu;

    public bool HUDOn = false;
    public GameObject HUD;



    private void Update()
    {
        if (IngameMenu != null && IngameMenu)
        {
            IngameMenu.SetActive(IngameMenuOpen);
        } 
        if (HUD != null && HUD)
        {
            HUD.SetActive(HUDOn); 
        }

    }
}
