using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(HealthBase))]
public class HealthBase_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        HealthBase myTarget = (HealthBase)target;

        if (GUILayout.Button("Damage"))
        {
            myTarget.Damage(1f);
        }
        if (GUILayout.Button("Damage%"))
        {
            myTarget.DamagePercent(50f);
        }
        if (GUILayout.Button("Heal"))
        {
            myTarget.Heal(1f);
        }
        if (GUILayout.Button("Heal%"))
        {
            myTarget.HealPercent(50f);
        }

        base.OnInspectorGUI();
    }
}
