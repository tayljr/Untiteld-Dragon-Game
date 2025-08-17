using UnityEditor;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    //not working on build
    public string scene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public delegate void TransitionScene(string Scene);
    public static event TransitionScene OnSceneTransitionEvent;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //transition
        if (other.gameObject.tag == "Player")
        {
            OnSceneTransitionEvent.Invoke(scene);
        }
    }
}
