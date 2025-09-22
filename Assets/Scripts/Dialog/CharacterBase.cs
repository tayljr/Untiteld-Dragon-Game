using System;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public bool isPlayer = false;
    public bool nearPlayer = false;
    public Sprite charPortrait;
    public string charName = "npc";
    public Color textColour = Color.black;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlayer)
        {
            nearPlayer = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !isPlayer)
        {
            nearPlayer = false;
        }
    }
}

