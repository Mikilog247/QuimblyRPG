using UnityEngine;
using System.Collections.Generic;

public static class HierarchyExtensions
{
    // Extension for Transform: GetChildren() returns list of direct children
    public static List<Transform> GetChildren(this Transform parent)
    {
        var children = new List<Transform>();
        foreach (Transform child in parent)
        {
            children.Add(child);
        }
        return children;
    }

    // Extension for GameObject: GetChildren() returns list of direct child GameObjects
    public static List<GameObject> GetChildren(this GameObject parent)
    {
        var children = new List<GameObject>();
        foreach (Transform child in parent.transform)
        {
            children.Add(child.gameObject);
        }
        return children;
    }
}
