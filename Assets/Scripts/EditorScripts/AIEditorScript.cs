using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(AIControllerEnemy))]
public class AIEditorScript : Editor
{

    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Go To Position"))
        {
            AIControllerEnemy controller = (AIControllerEnemy)target;
            controller.gotopos();
        }
        
    }
}
