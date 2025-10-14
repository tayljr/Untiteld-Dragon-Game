using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
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


    [SerializeField] private Animator _animator;
    [SerializeField] private float _fadeSpeed = 1f;
    private float _opacity = 0f;
    private bool _isTransitioning = false;

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
    public void StartSceneTransition(string sceneName)
    {
        if (!_isTransitioning)
        {
            StartCoroutine(SceneTransit(sceneName));
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void UIAnimationTrigger(System.Action action)
    {
        _animator.SetTrigger("trigger");
        action.Invoke();
    }
    public IEnumerator SceneTransit(string sceneName)
    {
        _isTransitioning = true;

        while (_opacity < 1f)
        {
            _opacity += Time.deltaTime * _fadeSpeed;
            _opacity = Mathf.Clamp01(_opacity);
            _animator.SetFloat("Opacity", _opacity);
            yield return null;
        }

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        while (!async.isDone)
        {
            yield return null;
        }
        while (_opacity > 0f)
        {
            _opacity -= Time.deltaTime * _fadeSpeed;
            _opacity = Mathf.Clamp01(_opacity);
            _animator.SetFloat("Opacity", _opacity);
            yield return null;
        }
        _isTransitioning = false;
    }
    private void OnEnable() => SceneTrigger.OnSceneTransitionEvent += StartSceneTransition;
    private void OnDisable() => SceneTrigger.OnSceneTransitionEvent -= StartSceneTransition;
}

