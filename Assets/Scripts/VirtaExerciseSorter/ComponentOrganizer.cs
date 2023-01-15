using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComponentOrganizer
{
    List<Component> OtherComponents = new List<Component>();
    Component Sorter;
    List<Component> VirtaComponents = new List<Component>();

    public List<Component> SortedComponents = new List<Component>();


    public void Sort(List<Component> components, VirtaComponentSortingMode mode)
    {
        List<Component> virtaUnsortedComponents = new List<Component>();

        components.ForEach((c) =>
        {
            if (c is VirtaMonoBehaviour) virtaUnsortedComponents.Add(c);
            else if (c is VirtaExerciseSorter) Sorter = c;
            else OtherComponents.Add(c);
        });
        switch (mode)
        {
            case VirtaComponentSortingMode.NameAscending:
                VirtaComponents = virtaUnsortedComponents   .OrderBy((c) => c.GetType().ToString())
                                                            .ToList();
                break;
            case VirtaComponentSortingMode.NameDescending:
                VirtaComponents = virtaUnsortedComponents   .OrderByDescending((c) => c.GetType().ToString())
                                                            .ToList();
                break;
            case VirtaComponentSortingMode.CategoryAscending:
                VirtaComponents = virtaUnsortedComponents   .OrderBy((c) => (c as VirtaMonoBehaviour).Category)
                                                            .ThenBy((c) => c.GetType().ToString())
                                                            .ToList();
                break;
            case VirtaComponentSortingMode.CategoryDescending:
                VirtaComponents = virtaUnsortedComponents   .OrderByDescending((c) => (c as VirtaMonoBehaviour).Category)  
                                                            .ThenBy((c) => c.GetType().ToString())
                                                            .ToList();
                break;
        }

        SortedComponents.AddRange(OtherComponents);
        SortedComponents.Add(Sorter);
        SortedComponents.AddRange(VirtaComponents);
    }
}
