using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComponentOrganizer
{
    public List<Component> OtherComponents = new List<Component>();
    public Component Sorter;
    public List<Component> VirtaComponents = new List<Component>();

    public ComponentOrganizer(List<Component> components)
    {
        components.ForEach((c) => {
            if(c is VirtaMonoBehaviour) VirtaComponents.Add(c);
            else if (c is VirtaExerciseSorter) Sorter = c;
            else OtherComponents.Add(c);
        });        
    }
}
