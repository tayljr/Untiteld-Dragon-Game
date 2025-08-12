using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneObject",menuName = "Untitled Dragon Game/Scene Object")]
public class SceneScriptableObject : ScriptableObject 
{
    public Vector3 SpawnPosition;
    public Vector3 SpawnRotation;
    public SceneAsset Scene;
    public Texture Icon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
