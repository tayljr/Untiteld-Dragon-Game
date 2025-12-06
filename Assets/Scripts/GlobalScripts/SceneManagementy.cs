using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;



#if UNITY_EDITOR
using UnityEditor;


[CustomEditor(typeof(SceneManagementy))]
public class SceneManagementyEditor : Editor
{
    

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SceneManagementy sceneManager = (SceneManagementy)target;
        if (GUILayout.Button("Add Current Scene"))
        {

        }
        if (GUILayout.Button("Set Editor Build Settings Scenes"))
        {
            // Find valid Scene paths and make a list of EditorBuildSettingsScene
            List<EditorBuildSettingsScene> editorBuildSettingsScenes = new List<EditorBuildSettingsScene>();
            foreach (var sceneAsset in sceneManager.m_SceneAssets)
            {
                string scenePath = AssetDatabase.GetAssetPath(sceneAsset);
                if (!string.IsNullOrEmpty(scenePath))
                    editorBuildSettingsScenes.Add(new EditorBuildSettingsScene(scenePath, true));
            }

            // Set the active platform or build profile scene list
            EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();

        }
    }

    
}
#endif

public class SceneManagementy : MonoBehaviour
{

#if UNITY_EDITOR
    public List<SceneAsset> m_SceneAssets = new List<SceneAsset>();
#endif
    [SerializeField] private float _fadeSpeed = 1f;

    [SerializeField]
    private Slider _loadingbar;
    [SerializeField]
    private TextMeshProUGUI _loadingtext;



    private static SceneManagementy instance;
    public static SceneManagementy Instance { get { return instance; } }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);

        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    
    public void StartSceneTransition(string sceneName, string lastActiveScene)
    {
        StartCoroutine(SceneTransit(sceneName, lastActiveScene));
    }
    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator SceneTransit(string sceneName, string lastActiveScene)
    {
        //load loading scene
        AsyncOperation loadLoadingScene = SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Additive);
        while (!loadLoadingScene.isDone)
        {
            yield return null;
        }
        //unload current scene
        Debug.LogWarning("Unloading Scene: " + lastActiveScene);
        AsyncOperation unloadCurrentOp = SceneManager.UnloadSceneAsync(lastActiveScene);
        while (!unloadCurrentOp.isDone)
        {
            yield return null;
        }

        _loadingbar = GameObject.Find("LoadingBar").GetComponent<Slider>();
        _loadingtext = GameObject.Find("LoadingText").GetComponent<TextMeshProUGUI>();

        AsyncOperation loadGameScene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        loadGameScene.allowSceneActivation = false;
        
        while (loadGameScene.progress < 0.9f)
        {
            float p = Mathf.Clamp01(loadGameScene.progress / 0.9f);
            _loadingbar.value = p;
            _loadingtext.text = "Loading " + (loadGameScene.progress * 100).ToString("F0") + "%";
            yield return null;
        }

        _loadingbar.value = 1f;
        _loadingtext.text = "Loading 100%";

        //activate scene
        loadGameScene.allowSceneActivation = true;

        while (!loadGameScene.isDone)
        {
            yield return null;
        }

        Scene loadedScene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(loadedScene);

        //unload loading scene
        AsyncOperation unloadOp = SceneManager.UnloadSceneAsync("Loading");
        while (!unloadOp.isDone)
        {
            yield return null;
        }

    }
    private void OnEnable() => SceneTrigger.OnSceneTransitionEvent += StartSceneTransition;
    private void OnDisable() => SceneTrigger.OnSceneTransitionEvent -= StartSceneTransition;
}

