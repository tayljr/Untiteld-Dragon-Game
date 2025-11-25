using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneMenu : MonoBehaviour
{
    public SceneList scene;
    public void LoadScene()
    {
        SceneManager.LoadScene(scene.ToString());
    }
    public void LoadSceneAdditive()
    {
        SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Additive);
    }
    public void UnLoadYourself()
    {// :3
               SceneManager.UnloadSceneAsync(scene.ToString());
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
