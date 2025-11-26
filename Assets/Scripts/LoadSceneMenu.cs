using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneMenu : MonoBehaviour
{

    public delegate void SceneLoadAction();
    public static event SceneLoadAction OnSceneLoad;


    public SceneList scene;
    public void LoadScene()
    {
        OnSceneLoad?.Invoke();
    }
    public void LoadSceneAdditive()
    {
        SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Additive);
    }
    public void QuitGame()
    {
        
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
    }

}
