using UnityEngine;

public class DialogPauseChecker : MonoBehaviour, IPauseable
{
    public bool isPaused = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPause()
    {
        isPaused = true;
    }

    public void OnResume()
    {
        isPaused =  false;
    }
}
