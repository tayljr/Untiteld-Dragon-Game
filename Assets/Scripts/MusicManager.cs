using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    
    public AudioSource audioSource;
    
    public List<SceneList> sceneList;
    public List<AudioClip> musicList = new List<AudioClip>();


    public static MusicManager Instance { get; private set; } 
    
    private void OnEnable() => SceneTrigger.OnSceneTransitionEvent += StartSceneTransition;
    
    private void OnDisable() => SceneTrigger.OnSceneTransitionEvent -= StartSceneTransition;
    
    private void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    
    
    private void StartSceneTransition(string newScene, string activeScene)
    {
        for (int i = 0; i < sceneList.Count; i++)
        {
            if (sceneList[i].ToString() == newScene)
            {
                audioSource.clip = musicList[i];
                audioSource.Play();
            }
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < sceneList.Count; i++)
        {
            if (sceneList[i].ToString() == SceneManager.GetActiveScene().name)
            {
                audioSource.clip = musicList[i];
                audioSource.Play();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
