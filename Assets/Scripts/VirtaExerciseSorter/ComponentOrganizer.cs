using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComponentOrganizer
{
    List<Component> OtherComponents = new List<Component>();
    Component Sorter;
    List<Component> VirtaComponents = new List<Component>();

    public List<Component> SortedComponents = new List<Component>();

    public void SortByNameDescending(List<Component> components){
        components.ForEach((c) => {
            if(c is VirtaMonoBehaviour) VirtaComponents.Add(c);
            else if (c is VirtaExerciseSorter) Sorter = c;
            else OtherComponents.Add(c);
        });  
        SortedComponents.AddRange(OtherComponents);
        SortedComponents.Add(Sorter);
        SortedComponents.AddRange(VirtaComponents);
    }
}
