using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VirtaExerciseSorter))]
public class VirtaExerciseSorterInspector : Editor
{
    private VirtaExerciseSorter sorter;
    private string errorText = "This script only works in prefab mode";

    public override void OnInspectorGUI()
    {
        if (UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() == null)
        {
            GUI.contentColor = Color.red;
            GUILayout.Label(errorText);
            return;
        }

        if (GUILayout.Button("Sort by name descending"))
        {
            SortComponents();
        }

        sorter = target as VirtaExerciseSorter;
        sorter.nameFilter = EditorGUILayout.TextField("filter", sorter.nameFilter);
        if (GUILayout.Button("Filter"))
        {
            HideComponents();
        }
    }

    private void SortComponents()
    {
        if (UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() == null)
        {
            Debug.LogError(errorText);
            return;
        }

        sorter = target as VirtaExerciseSorter;
        sorter.SortComponents();
    }

    private void HideComponents()
    {
        if (UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() == null)
        {
            Debug.LogError(errorText);
            return;
        }
        sorter = target as VirtaExerciseSorter;
        sorter.HideComponents();
        Repaint();
    }
}

