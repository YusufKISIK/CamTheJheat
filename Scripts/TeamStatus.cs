using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TeamStatus 
{
    public enum VisibilityValues
    {
        Visible,
        Invisible
    }
 
    public VisibilityValues Visibility;
    public bool Failed;
}