using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SkillTreePassiveSO))]
public class SkillTreePassiveSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SkillTreePassiveSO passive = (SkillTreePassiveSO)target;

        if (GUILayout.Button("Reset Tier"))
        {
            passive.ResetTier();
        }
    }
}
