using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public delegate void TransitionScene(string Scene, string ActiveScene);
    public static event TransitionScene OnSceneTransitionEvent;

    public SceneList scene;

    public void TriggerSceneFunction()
    {
        OnSceneTransitionEvent.Invoke(scene.ToString(), SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter(Collider other)
    {
        //transition
        if (other.gameObject.tag == "Player")
        {
            OnSceneTransitionEvent.Invoke(scene.ToString(), SceneManager.GetActiveScene().name);
        }
    }
}
