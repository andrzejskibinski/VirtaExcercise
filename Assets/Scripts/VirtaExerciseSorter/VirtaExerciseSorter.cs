using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]
public class VirtaExerciseSorter : MonoBehaviour
{

    public string nameFilter;
    public void SortComponents()
    {

    }

    public void HideComponents()
    {
        var organizer = new ComponentOrganizer(GetComponents<Component>().ToList());

        organizer.VirtaComponents.ForEach((c) =>
        {
            var check = c.GetType().ToString().ToLower().Contains(nameFilter.ToLower());
            c.hideFlags = check ? HideFlags.None : HideFlags.HideInInspector;
        });
    }
}
