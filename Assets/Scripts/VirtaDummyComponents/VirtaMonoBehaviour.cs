using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VirtaMonoBehaviour : MonoBehaviour
{
    public virtual VirtaComponentCategory Category { get { return VirtaComponentCategory.Coba; } }
}
