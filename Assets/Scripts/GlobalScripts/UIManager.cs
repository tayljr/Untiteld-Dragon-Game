using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _fadeSpeed = 1f;
    private float _opacity = 0f;
    private bool _isTransitioning = false;

    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }


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
           _animator = GetComponent<Animator>();    
    }
    public void StartSceneTransition(string sceneName)
    {
        if(!_isTransitioning)
        {
            StartCoroutine(SceneTransit(sceneName));
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void UIAnimationTrigger(System.Action action,string trigger)
    {
        _animator.SetTrigger(trigger);
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
        _isTransitioning= false;
    }
    private void OnEnable() => SceneTrigger.OnSceneTransitionEvent += StartSceneTransition;
    private void OnDisable() => SceneTrigger.OnSceneTransitionEvent -= StartSceneTransition;
}
