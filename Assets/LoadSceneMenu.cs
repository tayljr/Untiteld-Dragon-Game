using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneMenu : MonoBehaviour
{
    public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);

}
