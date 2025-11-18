using UnityEngine;

public class ToggleVFX : MonoBehaviour
{

    public bool vfxEnabled = true;
    private GameObject particle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (vfxEnabled)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public void ToggleVFXEnabled(bool state)
    {
        vfxEnabled = state;
    }
}
