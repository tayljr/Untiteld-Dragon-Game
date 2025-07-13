using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(AIController))]
public class AIEditorScript : Editor
{

    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Go To Position"))
        {
            AIController controller = (AIController)target;
            controller.gotopos();
        }
        
    }
}
