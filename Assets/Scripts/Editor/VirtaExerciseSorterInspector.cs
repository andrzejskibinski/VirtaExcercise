using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VirtaExerciseSorter))]
public class VirtaExerciseSorterInspector : Editor
{
    private VirtaExerciseSorter sorter;
    private string errorText = "Sorting only works in prefab mode";
    public override void OnInspectorGUI()
    {

        //unhappy code first
        if (UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() == null)
        {
            GUI.contentColor = Color.red;
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(errorText);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            return;
        }

        //sorting gui
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("Sorting");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Sort by name ascending"))
        {
            OnSortButton(VirtaComponentSortingMode.NameAscending);
        }
        if (GUILayout.Button("Sort by name descending"))
        {
            OnSortButton(VirtaComponentSortingMode.NameDescending);
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Sort by category ascending"))
        {
            OnSortButton(VirtaComponentSortingMode.CategoryAscending);
        }
        if (GUILayout.Button("Sort by category descending"))
        {
            OnSortButton(VirtaComponentSortingMode.CategoryDescending);
        }
        GUILayout.EndHorizontal();

        //filtering gui
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("Filtering");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        sorter = target as VirtaExerciseSorter;
        sorter.NameFilter = EditorGUILayout.TextField(sorter.NameFilter);
        if (GUILayout.Button("Filter"))
        {
            OnFilterButton();
        }
        GUILayout.EndHorizontal();

        sorter.CategoryFilter.Keys.ToList().ForEach((k) => sorter.SetCategory(k, GUILayout.Toggle(sorter.CategoryFilter[k], k.ToString())));

        //obvious label to mark the Virta scripts
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUI.contentColor = Color.green;
        GUILayout.Label("----Virta Components----");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }

    /// <summary>
    /// triggered by Sort button
    /// </summary>
    /// <param name="mode">Sorting mode</param>
    private void OnSortButton(VirtaComponentSortingMode mode)
    {
        if (UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() == null)
        {
            Debug.LogError(errorText);
            return;
        }

        sorter = target as VirtaExerciseSorter;
        sorter.SortComponents(mode);
    }

    /// <summary>
    /// triggered by Filter button
    /// </summary>
    private void OnFilterButton()
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
