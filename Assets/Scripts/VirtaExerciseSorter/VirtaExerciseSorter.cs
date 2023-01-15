using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]
public class VirtaExerciseSorter : MonoBehaviour
{
#if UNITY_EDITOR
    public string NameFilter = "";

    /// <summary>
    /// Example categories
    /// </summary>
    public Dictionary<VirtaComponentCategory, bool> CategoryFilter = new Dictionary<VirtaComponentCategory, bool>() {
        {VirtaComponentCategory.Coba, true},
        {VirtaComponentCategory.Mesh, true},
        {VirtaComponentCategory.Configurator, true},
        {VirtaComponentCategory.Label, true},
        {VirtaComponentCategory.Controller, true}
    };

    private List<Component> components;

    /// <summary>
    /// Sort componets based on the mode
    /// </summary>
    /// <param name="mode">Sorting mode</param>
    public void SortComponents(VirtaComponentSortingMode mode)
    {
        UnhideComponents();

        components = GetComponents<Component>().ToList();
        var organizer = new ComponentOrganizer();
        organizer.Sort(components, mode);

        bool done = false;
        while (!done)
        {
            done = true;

            components = GetComponents<Component>().ToList();

            for (int oldIndex = 0; oldIndex < components.Count; ++oldIndex)
            {
                var component = components[oldIndex];
                int newIndex = organizer.SortedComponents.IndexOf(component);
                if (oldIndex != newIndex)
                {
                    MoveComponent(component, oldIndex, newIndex);
                    done = false;
                    break;
                }
            }
        }

        HideComponents();
    }

    /// <summary>
    /// Set HideFlags based on filter text and categories
    /// </summary>
    public void HideComponents()
    {
        GetComponents<Component>().ToList().Where((c) => c is VirtaMonoBehaviour).ToList().ForEach((c) =>
        {
            var check = c.GetType().ToString().ToLower().Contains(NameFilter.ToLower()) && CategoryFilter[(c as VirtaMonoBehaviour).Category];
            c.hideFlags = check ? HideFlags.None : HideFlags.HideInInspector;
        });
    }

    /// <summary>
    /// Set categories dictionary and trigger hiding method
    /// </summary>
    /// <param name="category"></param>
    /// <param name="check"></param>
    public void SetCategory(VirtaComponentCategory category, bool check)
    {
        CategoryFilter[category] = check;
        HideComponents();
    }

    /// <summary>
    /// Compact way of moving components up or down
    /// </summary>
    /// <param name="component">Component to move</param>
    /// <param name="oldIndex">Index before sorting</param>
    /// <param name="newIndex">Index after sorting</param>
    private void MoveComponent(Component component, int oldIndex, int newIndex)
    {
        while (oldIndex < newIndex)
        {
            UnityEditorInternal.ComponentUtility.MoveComponentDown(component);
            ++oldIndex;
        }

        while (oldIndex > newIndex)
        {
            UnityEditorInternal.ComponentUtility.MoveComponentUp(component);
            --oldIndex;
        }
    }

    /// <summary>
    /// Unhide all components for sorting to work correctly. 
    /// Otherwise the MoveComponent might run into problem since the Unity's MoveUp and MoveDown don't recognize hidden inspector components
    /// </summary>
    private void UnhideComponents()
    {
        GetComponents<Component>().ToList().Where((c) => c is VirtaMonoBehaviour).ToList().ForEach((c) => c.hideFlags = HideFlags.None);
    }

#endif
}
